

using System.ComponentModel.DataAnnotations;

namespace project_service.Entities
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool isCompleted { get; set; }
    }
}
