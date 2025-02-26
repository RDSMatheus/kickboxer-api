using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KickboxerApi.DTOs
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}