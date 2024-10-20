using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_service.Dtos;
using project_service.Entities;
using project_service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentController> _logger;
        private readonly IConfiguration _config;
        public StudentController(IStudentService service, IMapper mapper, ILogger<StudentController> logger, IConfiguration config)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _config = config;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] string name = "")
        {
            var token = "";
            if (Request.Headers.TryGetValue("Authorization", out var authorization))
            {
                token = authorization.ToString();
            }
            _logger.LogInformation($"******** RAW token = {token}");

            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }

            _logger.LogInformation($"******** clean token = {token}");

            var parts = token.Split('.');

            if (parts.Length == 3)
            {
                // Get the second section (the payload)
                string payload = parts[1];
                Console.WriteLine($"Payload (Base64): {payload}");

                // Optionally decode the Base64 payload to a JSON string
                string jsonPayload = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                Console.WriteLine($"Decoded Payload: {jsonPayload}");
            }

            //validate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateIssuer = false,
                ValidIssuer = _config["Jwt:Issue"],
                ValidateAudience = false,
                ValidAudience = _config["Jwt:Audience"],
                ValidateLifetime = false,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            //SecurityToken validatedToken;
            //ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;

            var customClaimValue = principal.FindFirst("Name")?.Value;
            _logger.LogInformation($"----------> payload: {customClaimValue}");
            if (!string.IsNullOrEmpty(name)) 
            { 
                var student = await _service.GetStudentByName(name);
                return Ok(student);
            }
            var result = await _service.GetAllStudents();
            var students = _mapper.Map<IEnumerable<StudentDto>>(result);
            return Ok(students);
        }

        [HttpGet("kiwkiw")]
        public async Task<IActionResult> GetStudent() {
            var result = await _service.GetAllStudents();
            var student = _mapper.Map<IEnumerable<StudentDto>>(result);
            return Ok(student);
        }
        [HttpPost]
        public IActionResult AddStudent()
        {
            return Ok("add student");
        }
    }
}
