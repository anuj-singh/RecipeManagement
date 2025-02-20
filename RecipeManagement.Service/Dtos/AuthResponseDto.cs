using System;

namespace RecipeManagement.Service.Dtos
{
public class AuthResponseDto
{
    
    public string UserId { get; set; } = "";
    public string UserName { get; set; } ="";
    public string Email { get; set; } ="";
    public required string Role { get; set; } 
    public string Token { get; set; }="";
    public string RefreshToken { get; set; }="";
    public string Message {get;set;}="";
}
}