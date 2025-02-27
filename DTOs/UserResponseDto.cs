using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KickboxerApi.DTOs
{
    public class UserResponseDto
    {
        public bool Exists { get; set; }
        public string Message { get; set; } = "";
    }
}