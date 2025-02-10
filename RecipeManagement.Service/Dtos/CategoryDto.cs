using System;

namespace RecipeManagement.Service.Dtos 
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public required string CategoryName {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? LastModifiedUserId  { get; set; }
        public int CreatedBy { get; set; }

    }
}