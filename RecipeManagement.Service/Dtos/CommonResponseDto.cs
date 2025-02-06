
using System;

namespace RecipeManagement.Service.Dtos 
{
    public class CommonResponseDto
    {   
        public  string? Message {get; set;} 
         public  bool Status {get; set;}
          public  int  StatusCode {get; set;}
          public  int  Id {get; set;}
    }
}