using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.DTO;
using project_service.Entities;
using project_service.Utils;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        public readonly DataContext _context;
        public LibraryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("students")]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("students/{student_id}")]
        public async Task<IActionResult> GetStudentById(int student_id)
        {
            var student = await _context.Students.Include(s => s.BorrowedBooks).SingleOrDefaultAsync(s => s.student_id == student_id);
            if(student == null) {
                return NotFound();
            }

            var borrowedBooks = student.BorrowedBooks ?? new List<Book>();

            var studentDto = new StudentDto
            {
                student_id = student.student_id,
                student_name = student.student_name,
                borrowed_books = borrowedBooks.Select(b => new BookDto
                {
                    book_id = b.book_id,
                    book_title = b.book_title
                }).ToList()
            };
            return Ok(studentDto);   
        }

        [HttpGet("books")]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpPost("borrow")]
        public IActionResult BorrowBook([FromBody] RequestBody req)
        {
            var student = _context.Students.SingleOrDefault(s => s.student_id == req.student_id);
            if(student == null) 
            {
                return NotFound("Student not found");
            }
            try
            {
                student.BorrowBook(req.book_id, _context);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Book borrowed successfully");
        }

    }
}
