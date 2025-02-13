using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;


namespace RecipeManagement.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly RecipeDBContext _recipeDBContext;

        public UserRepository(RecipeDBContext recipeDBContext)
        {
            _recipeDBContext = recipeDBContext;
        }
        public async Task<List<Users>> GetAllUsersAsync()
        {
            var userList = await _recipeDBContext.Users.ToListAsync();
            return userList;
        }


        public async Task<Users> GetUsersByIdAsync(int id)
        {


            var getUser = await _recipeDBContext.Users
                                                .Where(u => u.UserId == id)
                                                .FirstOrDefaultAsync();
            if (getUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return getUser;


        }

        public async Task<Users> AddUser(Users user)
        {
            _recipeDBContext.Users.Add(user);
            await _recipeDBContext.SaveChangesAsync();
            return user;
        }


        public async Task<Users> loginUserBysync(string email, string password)
        {


            var loginUser = await _recipeDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (loginUser == null)
            {
                throw new KeyNotFoundException($"User with ID {email} not found.");
            }

            return loginUser;
        }

        public async Task<Users> UpdateUser(int id, Users user)
        {
            var exUser = await _recipeDBContext.Users.FindAsync(id);
            if (exUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            exUser.UserName = user.UserName;
            exUser.Bio = user.Bio;
            exUser.Email = user.Email;

            await _recipeDBContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _recipeDBContext.Users.FindAsync(id);
            if (user != null)
            {
                _recipeDBContext.Users.Remove(user);
                await _recipeDBContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
        //-----------------------------------------------REPO FOR CATEGORY-----------------------------------

        // public async Task<List<Category>> GetAllCategoryAsync()
        // {
        //     var categoryList = await _recipeDBContext.Categories.ToListAsync();
        //     return categoryList;
        // }


        // public async Task<Category> GetCategoryByIdAsync(int id)
        // {


        //     var catId = await _recipeDBContext.Categories
        //                                         .Where(c => c.CategoryId == id)
        //                                         .FirstOrDefaultAsync();
        //     if (catId == null)
        //     {
        //         throw new KeyNotFoundException($"Category with ID {id} not found.");
        //     }
        //     return catId;


        // }

        // public async Task<Category> CreateCategory(Category category)
        // {
        //     _recipeDBContext.Categories.Add(category);
        //     await _recipeDBContext.SaveChangesAsync();
        //     return category;
        // }

        // public async Task<Category> UpdateCategory(int CategoryId, Category category)
        // {
        //     var exCategory = await _recipeDBContext.Categories.FindAsync(CategoryId);
        //     if (exCategory == null)
        //     {
        //         throw new KeyNotFoundException($"Category with ID {CategoryId} not found.");
        //     }
        //     exCategory.CategoryName = category.CategoryName;

        //     await _recipeDBContext.SaveChangesAsync();
        //     return category;
        // }

        // public async Task<bool> DeleteCategory(int CategoryId)
        // {
        //     var category = await _recipeDBContext.Categories.FindAsync(CategoryId);
        //     if (category != null)
        //     {
        //         _recipeDBContext.Categories.Remove(category);
        //         await _recipeDBContext.SaveChangesAsync();
        //         return true;
        //     }
        //     return false;

        // }
        // //---------------------------------------REPO FOR USERROLE------------------------------------------------------------------







    }
}
