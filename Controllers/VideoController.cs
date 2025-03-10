using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KickboxerApi.DTOs;
using KickboxerApi.Models;
using KickboxerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace KickboxerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly VideoServices _videoServices;

        public VideoController(VideoServices videoServices)
        {

            _videoServices = videoServices;
        }

        [HttpPost("upload-video")]
        [Consumes("multipart/form-data")]
        async public Task<ActionResult> Post([FromForm] VideoDto video)
        {
            try
            {
                await _videoServices.Post(video);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{pageNumber?}/{pageSize?}")]
        async public Task<ActionResult<List<Video>>> GetAll(
        [SwaggerParameter(Required = false)][FromRoute] string pageNumber = "1",
        [SwaggerParameter(Required = false)][FromRoute] string pageSize = "10")
        {
            try
            {
                var videos = await _videoServices.GetAll(pageNumber, pageSize);

                return StatusCode(200, videos);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}