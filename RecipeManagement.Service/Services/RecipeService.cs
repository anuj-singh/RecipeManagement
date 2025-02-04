using RecipeManagement.Service.Interfaces;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Service.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            try
            {
                return await _recipeRepository.GetAllRecipesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all recipes.", ex);
            }
        }

        // Get a recipe by its ID
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            try
            {
                return await _recipeRepository.GetRecipeByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving recipe with ID {id}.", ex);
            }
        }

        // Get all recipes by a specific user ID
        public async Task<List<Recipe>> GetAllRecipebyUserIdAsync(int userId)
        {
            try
            {
                return await _recipeRepository.GetRecipesByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving recipes for User ID {userId}.", ex);
            }
        }

        // Create a new recipe
        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            try
            {
                return await _recipeRepository.AddRecipeAsync(recipe);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the recipe.", ex);
            }
        }

        // Update an existing recipe
        public async Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe)
        {
            try
            {
                return await _recipeRepository.UpdateRecipeAsync(id, recipe);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating recipe with ID {id}.", ex);
            }
        }

        // Delete a recipe by its ID
        public async Task<bool> DeleteRecipeAsync(int id)
        {
            try
            {
                return await _recipeRepository.DeleteRecipeAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting recipe with ID {id}.", ex);
            }
        }
    }
}
