using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KickboxerApi.DTOs;
using KickboxerApi.Models;
using KickboxerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KickboxerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserDto newUser)
        {
            try
            {

                await _usersService.Post(newUser);

                return StatusCode(201, new { newUser, message = "Usuário criado com sucesso" }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            try
            {
                var user = await _usersService.GetByEmail(email);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(string id, [FromBody] UserUpdateDto updatedUser)
        {
            try
            {
                await _usersService.Update(id, updatedUser);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _usersService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}