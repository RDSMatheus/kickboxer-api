using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KickboxerApi.Models;
using KickboxerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KickboxerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsersService _userService;
        public AuthController(IConfiguration configuration, UsersService usersService)
        {
            _configuration = configuration;
            _userService = usersService;
        }

        [HttpPost("login")]
        async public Task<ActionResult> Login(AuthenticateRequest userLogin)
        {
            try
            {
                if (string.IsNullOrEmpty(userLogin.Email))
                {
                    return Unauthorized(new { message = "Email vazio." });
                }
                if (string.IsNullOrEmpty(userLogin.Password))
                {
                    return Unauthorized(new { message = "Senha vazia." });
                }
                var validEmail = await _userService.GetByEmail(userLogin.Email);

                if (validEmail == null)
                {
                    return BadRequest(new { message = "Email não cadastrado." });
                }

                var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.Sub, userLogin.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}