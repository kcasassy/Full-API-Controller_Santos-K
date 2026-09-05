using System.ComponentModel.DataAnnotations;

namespace Full_API_Controller_Santos_K.DTOs
{
    public class EditStudentDto
    {
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