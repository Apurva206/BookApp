using System.ComponentModel.DataAnnotations;

namespace BookApp.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; } // Primary Key
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Author { get; set; }

        [Required]
        public string? ISBN { get; set; } 
        public int PublicationYear { get; set; }
    }
}
