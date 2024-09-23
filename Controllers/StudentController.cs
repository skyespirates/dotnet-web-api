using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_service.Dtos;
using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IMapper _mapper;
        public StudentController(IStudentService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] string name = "")
        {
            if (!name.IsNullOrEmpty()) 
            { 
                var student = await _service.GetStudentByName(name);
                return Ok(student);
            }
            var result = await _service.GetAllStudents();
            var students = _mapper.Map<IEnumerable<StudentDto>>(result);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<StudentDto> GetStudent(int id) {
            var result = await _service.GetStudentById(id);
            var student = _mapper.Map<StudentDto>(result);
            return student;
        }
        [HttpPost]
        public IActionResult AddStudent()
        {
            return Ok("add student");
        }
    }
}
