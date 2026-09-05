using System.ComponentModel.DataAnnotations;

namespace Full_API_Controller_Santos_K.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^STU-\d{4}$")]
        public string StudentNumber { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        public string FirstName { get; set; } = "";

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Birthplace { get; set; }
    }
}