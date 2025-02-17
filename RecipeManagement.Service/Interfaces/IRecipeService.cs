using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeManagement.Service.Dtos;

namespace RecipeManagement.Service.Interfaces
{
    public interface IRecipeService
    {
        Task<List<RecipeDTO>> GetAllRecipesAsync();
        Task<RecipeDTO> GetRecipeByIdAsync(int id);
        Task<List<RecipeDTO>> GetAllRecipebyUserIdAsync(int userId); 
        Task<CommonResponseDto> CreateRecipeAsync(RecipeCreateDto recipeCreateDto);
        Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe);
        Task<CommonResponseDto> DeleteRecipeAsync(int id);
    }
}
