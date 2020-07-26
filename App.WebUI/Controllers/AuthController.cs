using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using App.Data.Abstract;
using App.Entity.Models;
using App.Data.Dtos;

namespace App.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            try
            {


                if (await _authRepository.UserExists(userForRegisterDto.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userToCreate = new Users
                {
                    UserName = userForRegisterDto.UserName
                };

                var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return StatusCode(201);
        }


        //http://localhost:44330/api/Auth/login
        /*
        {
            "username":"Admin",
            "password":"12345"
        }

        Token : eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJBZG1pbiIsIm5iZiI6MTU5NTI0NzI4MywiZXhwIjoxNTk1MzMzNjgzLCJpYXQiOjE1OTUyNDcyODN9.f3TtPb0qIuxuQAhfN8lQJ_FT1H1cuMn24gkpFVEEpdXtFcW2R7g4gWh04iwEwmd4AjMUflVS3XBIOTsQVPr9jQ

        Key: Authorization
        Value: Caranin super gizli anahtari

        */
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }

    }
}
