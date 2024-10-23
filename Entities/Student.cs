using System;
using System.ComponentModel.DataAnnotations;
using project_service.Data;
using project_service.Dtos;

namespace project_service.Entities;

public class Student
{
    [Key]
    public int student_id {get; set;}
    public required string student_name {get; set;}
    public ICollection<Book>? BorrowedBooks {get; set;}
    public string?  createdBy { get; set; }

    public void BorrowBook(int book_id, DataContext context)
    {
        var book = context.Books.SingleOrDefault(b => b.book_id == book_id);
        if(book == null)
        {
            throw new Exception("Book not found");
        }
        if(book.borrower_id != null)
        {
            throw new Exception("Book already borrowed");
        }
        book.borrower_id = this.student_id;
        context.SaveChanges();
    }
}
