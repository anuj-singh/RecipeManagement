using System;

namespace RecipeManagement.Service.Dtos 
{
    public class RecipeFilterDto
    {
        public string Title { get; set; }="";
        public  string Ingredients {get; set;}="";
        public int UserId {get;set;}
        public int CategoryId {get;set;}

    }
}