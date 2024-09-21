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
        private readonly ILogger<LibraryController> _logger;
        public readonly DataContext _context;
        public LibraryController(DataContext context, ILogger<LibraryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("students")]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            var sql = "SELECT * FROM Students";
            var result = await _context.Students.FromSqlRaw(sql).ToListAsync();
            _logger.LogInformation("**** {result} **** kiwkiw {number}", result, 23);
            _logger.LogError(sql);
            _logger.LogWarning("Watch Out!!");
            return result;
        }

        [HttpPost("students")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest req)
        {
            var sql = "INSERT INTO Students (student_name) VALUES ({0})";
            var result = await _context.Database.ExecuteSqlRawAsync(sql, req.student_name);
            return Ok($"{result} students added successfully");
        }

        [HttpGet("students/{student_id}")]
        public async Task<IActionResult> GetStudentById(int student_id)
        {
            var student = await _context.Students.Include(s => s.BorrowedBooks).SingleOrDefaultAsync(s => s.student_id == student_id);

            var sql = "SELECT s.*, b.* FROM Students s LEFT JOIN Books b ON s.student_id = b.borrower_id WHERE student_id = {0}";
            var result = await _context.Students.FromSqlRaw(sql, student_id).FirstOrDefaultAsync();
            if(result == null) {
                return NotFound();
            }

            var studentWithBooks = new StudentWithBooks{
                student =  new StudentDto
                {
                    student_id = result.student_id,
                    student_name = result.student_name
                },
                books = result.BorrowedBooks?.Select(b => new Book
                {
                    book_id = b.book_id,
                    book_title = b.book_title
                })
            };
            /*
            var borrowedBooks = student.BorrowedBooks ?? [];

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
            */
            return Ok(studentWithBooks);   
        }

        [HttpGet("books")]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpPost("books")]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest req)
        {
            var sql = "INSERT INTO Books (book_title, author) VALUES ({0}, {1})";
            var result = await _context.Database.ExecuteSqlRawAsync(sql, req.book_title, req.author);
            if(result != 1) {
                return BadRequest("Fail to add book");
            }
            return Created($"/api/library/books", result);
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
