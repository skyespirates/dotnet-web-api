using System;

namespace project_service.DTO;

public class StudentDto
{
    public int student_id {get; set;}
    public string? student_name {get; set;}
    public List<BookDto>? borrowed_books {get; set;}
}
