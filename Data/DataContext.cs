using Microsoft.EntityFrameworkCore;
using project_service.Entities;
namespace project_service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Book> Books { get; set; }
     }
}
