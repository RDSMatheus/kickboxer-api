using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KickboxerApi.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    public class CloudinaryServices
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryServices(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<VideoUploadResult?> UploadVideoAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new VideoUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Folder = "Kickboxer"
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    return uploadResult;
                }

            }
            return null;
        }
    }
}