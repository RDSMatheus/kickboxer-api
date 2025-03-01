using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KickboxerApi.DTOs;
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

        public async Task<UserResponseDto> Post(User newUser)
        {
            var existingUser = await _usersCollection.Find(u => u.Email == newUser.Email).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return new UserResponseDto { Exists = true, Message = "E-mail já cadastrado." };
            }
            await _usersCollection.InsertOneAsync(newUser);
            return new UserResponseDto { Exists = false, Message = "Usuário cadastrado com sucesso." };
        }

        public async Task<User> GetById(string id)
        {
            var user = await _usersCollection.AsQueryable().Where(i => i.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User?> Update(string id, UserUpdateDto updatedUser)
        {
            var user = await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, id);
                var update = Builders<User>.Update.Set(u => u.Name, updatedUser.Name).Set(u => u.Email, updatedUser.Email);
                await _usersCollection.FindOneAndUpdateAsync(filter, update);
            }
            return user;
        }

        public async Task<User?> Delete(string id)
        {
            var user = await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();

            if (user != null)
            {
                await _usersCollection.DeleteOneAsync(i => i.Id == id);
            }
            return user;
        }
    }
}