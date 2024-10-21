using project_service.Entities;

namespace project_service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<int> AddUser(User user);
        Task<int> RegisterUser(string username, string password);
        Task<bool> Authenticate(User user);
    }
}
