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

        async public Task<User> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Envie um email válido.");
            }
            return await _userRepository.GetByEmail(email);
        }

        async public Task Update(string id, UserUpdateDto updatedUser)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Envie um id válido.");
            }
            await _userRepository.Update(id, updatedUser);

            var user = await _userRepository.Delete(id) ?? throw new Exception("Usuário não encontrado.");
        }

        async public Task Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Envie um id válido.");
            }
            var user = await _userRepository.Delete(id) ?? throw new Exception("Usuário não encontrado.");
        }
    }
}