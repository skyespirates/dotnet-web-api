using project_service.Entities;
using project_service.Interfaces;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;

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
        public Task<User> GetUserByName(string name)
        {
            return _repository.GetUserByName(name);
        }
        public  async Task<int> AddUser(User user)
        {
            return await _repository.AddUser(user);
        }
        public async Task<int> RegisterUser(string username, string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Name = username, Password = hashedPassword };
            return await _repository.AddUser(user);
        }
        public Task<bool> Authenticate(User user)
        {
            return _repository.AuthenticateUser(user);
        }

    }
}
