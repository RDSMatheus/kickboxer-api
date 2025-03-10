using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KickboxerApi.Models;
using KickboxerApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KickboxerApi.Repository
{
    public class VideoRepository
    {
        private readonly IMongoCollection<Video> _videoCollection;
        public VideoRepository(IOptions<KickboxerDatabaseSettings> kickboxerDatabaseSettings)
        {

            var mongoClient = new MongoClient(kickboxerDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(kickboxerDatabaseSettings.Value.DatabaseName);
            _videoCollection = mongoDatabase.GetCollection<Video>(kickboxerDatabaseSettings.Value.VideosCollectionName);

        }

        async public Task Post(Video video)
        {
            await _videoCollection.InsertOneAsync(video);

        }

        async public Task<List<Video>> GetAll(int limit, int pageNumber)
        {
            return await _videoCollection.Find(_ => true).Skip((pageNumber - 1) * limit).Limit(limit).ToListAsync();
        }
    }
}