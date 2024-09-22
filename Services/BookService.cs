using Microsoft.AspNetCore.Mvc;
using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _repository.GetAllBooks();
        }
        public async Task<Book> GetBookAsync(int id)
        {
            return await _repository.GetBookById(id);
        }
        public async Task AddBook(Book book)
        {
            await _repository.AddBook(book);
        }
    }
}
