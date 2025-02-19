using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeManagement.Data.Models 
{
    public class PasswordResetToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }      
        public string Token { get; set; }    
        public DateTime ExpirationDate { get; set; } 
        public User User { get; set; } 
    }
}