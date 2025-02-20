using System;

namespace RecipeManagement.Service.Dtos 
{
public class AuthRequestDto
{
    
    public required string Email { get; set; } 
    public required string Password { get; set; } 
}
}