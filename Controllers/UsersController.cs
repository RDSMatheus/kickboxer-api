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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            try
            {
                var user = await _usersService.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}