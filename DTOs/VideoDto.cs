using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KickboxerApi.DTOs
{
    public class VideoDto
    {
        [Required(ErrorMessage = "O titulo é obrigatório.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "O campo description é obrigatório.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Envie o video.")]
        public IFormFile? File { get; set; }
    }
}