using Microsoft.EntityFrameworkCore;
using project_service.Entities;
namespace project_service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasMany(s => s.BorrowedBooks)
            .WithOne(b => b.borrower)
            .HasForeignKey(b => b.borrower_id)
            .IsRequired(false);

            modelBuilder.Entity<Student>().HasData(
                new Student { student_id = 5, student_name = "Zilong" },
                new Student { student_id = 6, student_name = "Freya" },
                new Student { student_id = 7, student_name = "Martis" }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { book_id = 10, book_title = "One Piece", author = "Eichirou Oda", borrower_id = 5 },
                new Book { book_id = 11, book_title = "Shingeki No Kyoujin", author = "Hajime Isayama", borrower_id = 5 },
                new Book { book_id = 12, book_title = "One Punch Man", author = "Yusuke Murate", borrower_id = 6 }
                );

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "skyes", Password = "test123" }
                );
        }
    }
}
