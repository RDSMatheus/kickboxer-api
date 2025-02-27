using KickboxerApi.DTOs;
using KickboxerApi.Models;
using KickboxerApi.Repository;


namespace KickboxerApi.Services
{
    using BCrypt.Net;
    public class UsersService
    {
        private readonly UsersRepository _userRepository;
        public UsersService(UsersRepository usersRepository)
        {
            _userRepository = usersRepository;
        }

        async public Task<string> Post(UserDto newUser)
        {
            if (newUser.ConfirmPassword != newUser.Password)
            {
                throw new Exception("As senhas não coincidem.");
            }

            string PasswordHash = BCrypt.HashPassword(newUser.Password);

            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = PasswordHash
            };
            var response = await _userRepository.Post(user);

            if (response.Exists)
            {
                throw new Exception(response.Message);
            }
            return response.Message;
        }

        async public Task<User> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }
    }
}