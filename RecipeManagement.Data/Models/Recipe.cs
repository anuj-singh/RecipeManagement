using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeManagement.Data.Models
{
        public class Recipe
        {
                 [Key]
                [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
                public int RecipeId { get; set; }
 
                public int UserId { get; set; }
 
                public  User? User { get; set; }=null;
                
                [Required]
                [MaxLength(200)]
                public  string Title { get; set; }="";
 
                [MaxLength(1000)]
                public string? Description { get; set; }
 
                [MaxLength(2000)]
                public string? Ingredients { get; set; }
 
                [Required]
                public int CookingTime { get; set; }
 
                [MaxLength(2000)]
                public string? Instructions { get; set; }
 
                [MaxLength(500)]
                public string? ImageUrl { get; set; }
 
                [Required]
                public int StatusId { get; set; }
 
                public int CategoryId { get; set; }
                public  Category? Category { get; set; }
 
                public DateTime CreatedAt { get; set; }
 
                public DateTime? UpdatedAt { get; set; }
 
                public int? LastModifiedUserId { get; set; }
 
                public int CreatedBy { get; set; }
 
 
        }
}