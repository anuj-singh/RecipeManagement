
using System;

namespace RecipeManagement.Service.Dtos 
{
    public class LoginDto
    {   
        public  string UserName {get; set;}="";
        // public  int UserId {get; set;}
         public  required string Email {get; set;}
          public required string Password {get; set;}
    }
}