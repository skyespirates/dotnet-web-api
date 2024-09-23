using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var students = await _repository.GetAllStudents();
            return students;
        }
        public async Task<Student> GetStudentById(int id)
        {
            var student = await _repository.GetStudentById(id);
            return student;
        }
        public async Task<Student> GetStudentByName(string name)
        {
            var student = await _repository.GetStudentByName(name);
            return student;
        }
        public async Task AddStudent(Student student)
        {
            await _repository.AddStudent(student);
        }
    }
}
