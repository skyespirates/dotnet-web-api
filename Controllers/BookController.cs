using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Entities;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController: ControllerBase
    {
        private readonly TodoContext _context;

        public BookController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }
    }
}