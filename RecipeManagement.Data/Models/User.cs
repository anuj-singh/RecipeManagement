using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeManagement.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }="";
        [Required]
        [EmailAddress]
        public  string Email { get; set; }="";
        [Required]
        public  string PasswordHash { get; set; }="";
        [MaxLength(2000)]
        public string? Bio {get; set;}
         public string? ImageUrl { get; set; }
        public int StatusId {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? LastModifiedUserId  { get; set; }
        public int CreatedBy { get; set; }
        public List<Recipe>? Recipes {get;set;}

        public string? PasswordResetToken { get; set; }

        public DateTime? PasswordResetTokenExpiration { get; set; }

        // Foreign Key
        public int SecurityQuestionId { get; set; }

        // Navigation property for the related SecurityQuestion
        public SecurityQuestion? SecurityQuestion { get; set; }
    }
}
