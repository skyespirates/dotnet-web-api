using project_service.Entities;

namespace project_service.Interfaces
{
    public interface ILibraryService
    {
        Task BorrowBook(int book_id, int student_id);
    }
}
