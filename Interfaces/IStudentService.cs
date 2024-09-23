using project_service.Entities;

namespace project_service.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByName(string name);
        Task AddStudent(Student student);

    }
}
