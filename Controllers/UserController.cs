using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using project_service.Entities;
using project_service.Interfaces;
using project_service.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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

        [HttpPost("register")]
        public Task Register([FromBody] User user)
        {
            return _service.AddUser(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest body)
        {
            _logger.LogInformation($"************************ result: {body.username}");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}
