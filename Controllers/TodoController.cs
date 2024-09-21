using Microsoft.AspNetCore.Mvc;
using project_service.Interfaces;
using project_service.Entities;
using AutoMapper;
using project_service.Dtos;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly IMapper _mapper;

        public TodoController(ITodoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetTodos()
        {
            var rawTodos = await _service.GetAllTodosAsync();
            var Todos = _mapper.Map<IEnumerable<TodoDto>>(rawTodos);
            return Ok(Todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _service.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo([FromBody] Todo todo)
        {
            await _service.AddTodoAsync(todo);
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] Todo updatedTodo)
        {
            if (id != updatedTodo.Id)
            {
                return BadRequest();
            }
            await _service.UpdateTodoAsync(updatedTodo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _service.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
