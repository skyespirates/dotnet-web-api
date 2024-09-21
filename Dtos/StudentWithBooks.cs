using System;
using project_service.Entities;

namespace project_service.Dtos;

public class StudentWithBooks
{
    public required StudentDto student {get; set;}
    public IEnumerable<Book>? books {get; set;}
}
