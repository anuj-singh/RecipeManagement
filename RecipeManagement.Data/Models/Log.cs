using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeManagement.Data.Models 
{
    public class Log
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId {get; set;}
        
        public  string ErrorInformation {get; set;}="";
        public DateTime CreatedAt { get; set; }
        public string FunctionName { get; set; }="";
       
    }
}