using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KickboxerApi.DTOs;
using KickboxerApi.Models;
using KickboxerApi.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KickboxerApi.Services
{
    public class UsersService
    {
        private readonly UsersRepository _userRepository;
        public UsersService(UsersRepository usersRepository)
        {
            _userRepository = usersRepository;
        }

        async public Task Post(User user)
        {
            await _userRepository.Post(user);
        }

        async public Task<User> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }
    }
}