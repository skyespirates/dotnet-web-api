using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Entities;
using project_service.Interfaces;

namespace project_service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var students = await _context.Users.ToListAsync();
            return students;
        }
        public async Task<User> GetUserById(int id)
        {
            var student = await _context.Users.FindAsync(id);
            return student;
        }
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> AuthenticateUser(User user)
        {
            var result = await _context.Users.SingleOrDefaultAsync(u => u.Name == user.Name);
            if (result == null) return false;
            if(result.Password != user.Password) return false;
            return true;
        }
    }
}
