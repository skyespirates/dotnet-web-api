using project_service.Entities;

namespace project_service.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task AddBook(Book book);
    }
}
