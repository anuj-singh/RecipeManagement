using System;

namespace RecipeManagement.Service.Dtos 
{
    public class UserDto
    {
        public int UserId { get; set; }
        public  string UserName { get; set; }="";
        public  string Email { get; set; }="";
        public  string PasswordHash { get; set; } ="";
        public string? Bio {get; set;}
        public string ImageCreateOrUpdate {get;set;}="";
         public string? ImageUrl { get; set; }
        public int StatusId {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? LastModifiedUserId  { get; set; }
        public int CreatedBy { get; set; }
        public string SecurityQuestion { get; set; } ="";
        public string SecurityAnswer { get; set; }="";
         public List<RecipeDTO>? Recipes {get;set;}

        
    }
}