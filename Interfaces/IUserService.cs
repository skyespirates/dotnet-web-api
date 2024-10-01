using project_service.Entities;

namespace project_service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task<bool> Authenticate(User user);
    }
}
