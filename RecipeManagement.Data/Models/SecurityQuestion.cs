using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeManagement.Data.Models
{
    public class SecurityQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SecurityQuestionId { get; set; }

        // Foreign Key to User
        public int UserId { get; set; }

        [Required]
        public string Question { get; set; } = "";

        [Required]
        public string Answer { get; set; } = "";

        // Navigation property for the related User
        public User? User { get; set; }
    }
}
