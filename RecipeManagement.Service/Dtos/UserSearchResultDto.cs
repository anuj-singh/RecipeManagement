using System;

namespace RecipeManagement.Service.Dtos 
{
    public class UserSearchResultDto
    {
        public int UserId { get; set; }
        public  string UserName { get; set; }="";
        public  string Email { get; set; }="";
        public string? Bio {get; set;}
         public string? ImageUrl { get; set; }
        public string StatusName {get; set;}="";
        public List<RecipeDTO>? Recipes {get;set;}
    }
}