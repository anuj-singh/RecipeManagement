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
     public DbSet<Log> Logs { get; set; }
     public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
     public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
   
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             if (!optionsBuilder.IsConfigured)
             {
                var contentRootPath = Directory.GetCurrentDirectory();
                var projectRootPath = Directory.GetParent(contentRootPath).FullName;
                var databasePath = Path.Combine(projectRootPath, "RecipeManagement.Data", "recipes.db");
                optionsBuilder.UseSqlite($"Data Source={databasePath}");
            }
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship between User and SecurityQuestion
            modelBuilder.Entity<User>()
                .HasOne(u => u.SecurityQuestion)  // User has one SecurityQuestion
                .WithOne(sq => sq.User)           // SecurityQuestion has one User
                .HasForeignKey<User>(u => u.SecurityQuestionId) // The foreign key property in User
                .OnDelete(DeleteBehavior.Cascade); // Optional: configure cascade delete behavior (can be modified)
        }


}

}


