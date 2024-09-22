using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Repositories
{
    public class BookRepository : IBookRepository
    {
        public readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }
        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book;
        }
        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBook(Book book)
        {

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
