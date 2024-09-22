using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        public LibraryService(IBookRepository bookRepository, IStudentRepository studentRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
        }

        public async Task RegisterStudent(Student student) 
        { 
            await _studentRepository.AddStudent(student);
        }
        public async Task RegisterBook(Book book)
        {
            await _bookRepository.AddBook(book);
        }
        public async Task BorrowBook(int book_id, int student_id)
        {
            var book = await _bookRepository.GetBookById(book_id);
            if (book == null) throw new KeyNotFoundException("Book not found");
            var student = await _studentRepository.GetStudentById(student_id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            book.borrower_id = student.student_id;
            await _bookRepository.UpdateBook(book);
        }
    }
}
