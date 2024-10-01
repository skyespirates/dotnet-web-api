using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<User>> GetAllUsers()
        { 
            return _repository.GetAllUsers();
        }
        public Task<User> GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }
        public Task AddUser(User user)
        {
            return _repository.AddUser(user);
        }
        public Task<bool> Authenticate(User user)
        {
            return _repository.AuthenticateUser(user);
        }
    }
}
