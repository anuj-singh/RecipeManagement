using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeManagement.Service.Dtos;

namespace RecipeManagement.Service.Interfaces
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<List<Recipe>> GetAllRecipebyUserIdAsync(int userId); 
        Task<CommonResponseDto> CreateRecipeAsync(Recipe recipe);
        Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe);
        Task<CommonResponseDto> DeleteRecipeAsync(int id);
         Task<List< RecipeSearchResultDto>> SearchRecipe(RecipeFilterDto filter);

    }
}
