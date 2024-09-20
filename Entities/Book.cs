using System.ComponentModel.DataAnnotations;

namespace project_service.Entities
{
    public class Book
    {
        [Key]
        public int book_id {get; set;}
        public string book_title {get; set;}

        public string author {get; set;}
    }
    
}