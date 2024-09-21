

namespace project_service.Dtos
{
    public class BookDto
    {
        public required int book_id { get; set; }
        public required string book_title { get; set; }
        public StudentDto borrower { get; set; }

    }
}

