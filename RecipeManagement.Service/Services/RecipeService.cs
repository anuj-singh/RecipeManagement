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
            // Fetch all recipes from the repository
            return await _recipeRepository.GetAllRecipesAsync();
        }

        // Get a recipe by its ID
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            // Fetch the recipe by ID from the repository
            return await _recipeRepository.GetRecipeByIdAsync(id);
        }

        // Get all recipes by a specific user ID
        public async Task<List<Recipe>> GetAllRecipebyUserIdAsync(int userId)
        {
            // Fetch recipes by user ID from the repository
            return await _recipeRepository.GetRecipesByUserIdAsync(userId);
        }

        // Create a new recipe
        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            // Add the new recipe using the repository
            return await _recipeRepository.AddRecipeAsync(recipe);
        }

        // Update an existing recipe
        public async Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe)
        {
            // Update the recipe using the repository
            return await _recipeRepository.UpdateRecipeAsync(id, recipe);
        }

        // Delete a recipe by its ID
        public async Task<bool> DeleteRecipeAsync(int id)
        {
            // Delete the recipe using the repository
            return await _recipeRepository.DeleteRecipeAsync(id);
        }
    }
}
