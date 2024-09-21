using System;

namespace project_service.Utils;

public class AddBookRequest
{
    public required string book_title {get; set;}
    public string? author {get; set;}
}
