using Microsoft.EntityFrameworkCore;
using project_service.Entities;
namespace project_service.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Book> Books { get; set; }
     }
}
