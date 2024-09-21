using System;
using project_service.Entities;

namespace project_service.DTO;

public class StudentWithBooks
{
    public required StudentDto student {get; set;}
    public IEnumerable<Book>? books {get; set;}
}
