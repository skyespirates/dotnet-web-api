using project_service.Entities;

namespace project_service.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task<bool> AuthenticateUser(User user);
    }
}
