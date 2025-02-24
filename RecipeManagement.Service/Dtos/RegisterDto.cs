namespace RecipeManagement.Service.Dtos
{
    public class RegisterDto
    {
        public int RoleId { get; set; }
        public LoginDto? LoginDto { get; set; }
        public string SecurityQuestion { get; set; } = "";
        public string SecurityAnswer { get; set; } = "";
    }
}