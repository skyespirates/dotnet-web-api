using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using project_service.Dtos;
using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController: ControllerBase
    {

        public readonly IBookService _service;
        public readonly IMapper _mapper;
        public BookController(IBookService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var rawBooks = await _service.GetBooksAsync();
            var books = _mapper.Map<IEnumerable<BookDto>>(rawBooks);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var rawBook = await _service.GetBookAsync(id);
            var book = _mapper.Map<BookDto>(rawBook);
            return Ok(book);
        }
    }
}