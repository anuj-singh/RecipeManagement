using System;

namespace RecipeManagement.Service.Dtos 
{
    public class RecipeSearchResultDto
    {
        public int RecipeId { get; set; }
        public  string Title {get; set;}="";
        public string Ingredients {get;set;}="";
        public CategoryDto? Category {get;set;}
        public UserDto?  User {get;set;}

    }
}