using System;

namespace RecipeManagement.Data.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Ingredients { get; set; }
        public int CookingTime { get; set; }
        public string? Instructions { get; set; }
        public string? ImageUrl { get; set; }
        public int StatusID { get; set; }
        public int CategoryID { get; set; }
        public required Category Category { get; set; }  // Related to Category entity
        public int UserId { get; set; }
        public required User User { get; set; }  // Related to User entity
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
