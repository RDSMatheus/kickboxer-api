using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KickboxerApi.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}