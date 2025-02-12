using Microsoft.EntityFrameworkCore;
using RecipeManagement.Data.Models;

namespace  RecipeManagement.Data {
public partial class RecipeDBContext : DbContext
{
     public RecipeDBContext() {}
  
     public RecipeDBContext (DbContextOptions< RecipeDBContext > options) : base(options)
     { } 

  
     public DbSet<User> Users { get; set; } 
     public DbSet<Role> Roles { get; set; } 
     public DbSet<UserRole> UserRoles { get; set; } 
     public DbSet<Category> Categories { get; set; } 
     public DbSet<Recipe> Recipes { get; set; } 

   
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             if (!optionsBuilder.IsConfigured)
             {
                
             }
        }
}

}


