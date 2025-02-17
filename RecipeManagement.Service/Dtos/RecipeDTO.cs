namespace RecipeManagement.Service.Dtos
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; } 
        public required string Title { get; set; } 
        public string? Description { get; set; } 
        public string? Ingredients { get; set; } 
        public int CookingTime { get; set; } 
        public string? Instructions { get; set; } 
        public string? ImageUrl { get; set; } 
        public int StatusId { get; set; } 
        public int CategoryId { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } 
        public int CreatedBy { get; set; }
        public int? LastModifiedUserId { get; set; } 
    }
}
