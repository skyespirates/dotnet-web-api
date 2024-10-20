﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using project_service.Entities;
using project_service.Interfaces;
using project_service.Utils;
using project_service.Utils.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Collections;
using project_service.Utils.Request;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;
        public UserController(IUserService service, ILogger<UserController> logger, IConfiguration config) 
        {
            _service = service;
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public Task<IEnumerable<User>> GetAllUsers()
        {
            return _service.GetAllUsers();
        }

        [HttpGet("{id}")]
        public Task<User> GetUserById(int id) 
        {
            return _service.GetUserById(id);
        }

        //[HttpPost("register")]
        //public Task Register([FromBody] RegisterUser user)
        //{
        //    return _service.AddUser();
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest body)
        {
            _logger.LogInformation($"['info', 'Login']");
            //byte[] secret = Convert.FromBase64String(_config["Jwt:Key"]);
            //string decodedString = Encoding.UTF8.GetString(secret);
            //var securityKey = new SymmetricSecurityKey(secret);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, body.username),
                new Claim(ClaimTypes.Email, "skyes@email.com"),
                new Claim(ClaimTypes.Surname, "crawford")
            };

            var accToken = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.UtcNow.AddMinutes(60),
              signingCredentials: credentials);

            var refToken = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.UtcNow.AddMinutes(360),
              signingCredentials: credentials);


            var accessToken = new JwtSecurityTokenHandler().WriteToken(accToken);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(refToken);
            Token tokens = new Token { AccessToken = accessToken, RefreshToken = refreshToken };
            return Ok(tokens);  
        }
    }
}
