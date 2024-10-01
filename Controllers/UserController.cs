using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using project_service.Entities;
using project_service.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService service, ILogger<UserController> logger) 
        {
            _service = service;
            _logger = logger;
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
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var result =  await _service.Authenticate(user);
            _logger.LogInformation($"************************ result: {result}");
            if (result == false) return Unauthorized("Unauthorized");
            return Ok("Login successfully");
        }
    }
}
