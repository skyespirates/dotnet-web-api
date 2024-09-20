using Microsoft.EntityFrameworkCore;
using project_service.Entities;
namespace project_service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasMany(s => s.BorrowedBooks)
            .WithOne(b => b.borrower)
            .HasForeignKey(b => b.borrower_id)
            .IsRequired(false);

        }
    }
}
