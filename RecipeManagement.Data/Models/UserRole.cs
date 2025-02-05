using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeManagement.Data.Models 
{
        public class UserRole
        {
                [Key]
                [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
                public int UserRoleId { get; set; }
 
                 public  int UserId { get; set; }
                public required User User { get; set; }
 
                public  int RoleId { get; set; }
                public required Role Role { get; set; }
 
                public DateTime CreatedAt { get; set; }
 
                public DateTime? UpdatedAt { get; set; }
 
                public int? LastModifiedUserId { get; set; }
 
                public int CreatedBy { get; set; }       }
}