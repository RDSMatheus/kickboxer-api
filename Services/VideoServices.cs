using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KickboxerApi.DTOs;
using KickboxerApi.Models;
using KickboxerApi.Repository;

namespace KickboxerApi.Services
{
    public class VideoServices
    {
        private readonly VideoRepository _videoRepository;
        private readonly CloudinaryServices _cloudinaryServices;

        public VideoServices(VideoRepository videoRepository, CloudinaryServices cloudinaryServices)
        {
            _videoRepository = videoRepository;
            _cloudinaryServices = cloudinaryServices ?? throw new ArgumentNullException(nameof(cloudinaryServices));

        }

        async public Task Post(VideoDto newVideo)
        {
            if (newVideo.File == null || newVideo.File.Length == 0)
            {
                throw new Exception("Video não enviado");
            }

            var url = await _cloudinaryServices.UploadVideoAsync(newVideo.File);

            var video = new Video
            {
                Title = newVideo.Title,
                Description = newVideo.Description,
                Url = url?.Url?.ToString() ?? throw new Exception("Failed to upload video")

            };

            await _videoRepository.Post(video);
        }

        async public Task<List<Video>> GetAll(string quantity, string pageNumber)
        {
            if (!int.TryParse(quantity, out int page))
            {
                page = 1;
            }

            if (!int.TryParse(pageNumber, out int pageSize))
            {
                pageSize = 10;

            }

            return await _videoRepository.GetAll(pageSize, page);

        }
    }
}