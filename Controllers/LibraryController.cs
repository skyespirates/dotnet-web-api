using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Entities;
using project_service.Interfaces;
using project_service.Utils;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<LibraryController> _logger;
        public readonly ILibraryService _service;
        public LibraryController(ILibraryService service, ILogger<LibraryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] RequestBody req)
        {
            try
            {
                await _service.BorrowBook(req.book_id, req.student_id);
                return Ok();

            }
            catch (Exception e)
            {
                Console.WriteLine($"Bad request {e.Message}");
                throw;
                
            }
        }
    }
}
