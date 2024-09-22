using System;
using System.ComponentModel.DataAnnotations;

namespace project_service.Utils;

public class AddBookRequest
{
    [Required]
    public required string book_title {get; set;}
    public string? author {get; set;}
}
