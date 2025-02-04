namespace RecipeManagement.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? Bio { get; set; }
        public int StatusID { get; set; }
        public Status Status { get; set; }  // Related to Status enum
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int LastModifiedUserID { get; set; }
        public int CreatedBy { get; set; }
    }
}
