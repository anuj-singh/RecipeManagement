namespace RecipeManagement.Data.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int LastModifiedUserID { get; set; }
        public int CreatedBy { get; set; }
    }
}
