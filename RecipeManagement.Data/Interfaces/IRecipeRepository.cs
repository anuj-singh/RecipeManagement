using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Data.Interfaces
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<List<Recipe>> GetRecipesByUserIdAsync(int userId);
        Task<Recipe> AddRecipeAsync(Recipe recipe);
        Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe);
        Task<bool> DeleteRecipeAsync(int id);
        Task<List<Recipe>> SearchRecipeByFilter(string title, string ingredient,int userId, int categoryId);

    }
}
