using System;

namespace RecipeManagement.Service.Dtos 
{
    public class UserFilterDto
    {
        public  string UserName { get; set; }="";
        public  string Email { get; set; }="";
        public int StatusId {get; set;}
        
    }
}