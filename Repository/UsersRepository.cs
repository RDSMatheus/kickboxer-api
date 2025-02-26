using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KickboxerApi.Models;
using KickboxerApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace KickboxerApi.Repository
{
    public class UsersRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersRepository(IOptions<KickboxerDatabaseSettings> kickboxerDatabaseSettings)
        {
            var mongoClient = new MongoClient(kickboxerDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(kickboxerDatabaseSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(kickboxerDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task Post(User newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

        public async Task<User> GetById(string id)
        {
            var user = await _usersCollection.AsQueryable().Where(i => i.Id == id).FirstOrDefaultAsync();
            return (User)user;
        }
    }
}