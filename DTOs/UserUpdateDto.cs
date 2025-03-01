using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KickboxerApi.DTOs
{
    public class UserUpdateDto
    {
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}