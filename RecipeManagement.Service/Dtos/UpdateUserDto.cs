using System;

namespace RecipeManagement.Service.Dtos 
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public  string UserName { get; set; }="";
        public  string Email { get; set; }="";
        public string? Bio {get; set;}
         public string? ImageUrl { get; set; }
        public int StatusId {get; set;}
        
    }
}