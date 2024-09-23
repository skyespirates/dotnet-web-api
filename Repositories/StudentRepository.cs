using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public readonly DataContext _context;
        public StudentRepository(DataContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return students;
        }
        public async Task<Student> GetStudentById(int id)
        {
            var student = await _context.Students.Include(s => s.BorrowedBooks).FirstOrDefaultAsync(s => s.student_id == id);
            if(student == null) throw new KeyNotFoundException("Student not found");
            return student;
        }
        public async Task<Student> GetStudentByName(string name)
        {
            var student = await _context.Students.Where(s => s.student_name == name).FirstOrDefaultAsync();
            if (student == null) throw new KeyNotFoundException("Student not found");
            return student;
        }
        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudent(Student s)
        {
            var student = await _context.Students.FindAsync(s.student_id);
            if (student == null) {
                throw new KeyNotFoundException($"Book with ID {s.student_id} not found.");
            }
            _context.Students.Update(student);
            await _context.SaveChangesAsync();  
        }
        public async Task DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
