namespace RecipeManagement.Service.Dtos
{
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
