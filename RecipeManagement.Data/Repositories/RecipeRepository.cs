using Microsoft.EntityFrameworkCore;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _context;

        public RecipeRepository(RecipeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.Include(r => r.User).Include(r => r.Category).ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var recipe = await _context.Recipes
                        .Include(r => r.User)
                        .Include(r => r.Category)
                        .FirstOrDefaultAsync(r => r.RecipeId == id);

        if (recipe == null)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.");
            }

        return recipe;
        }

        public async Task<List<Recipe>> GetRecipesByUserIdAsync(int userId)
        {
            return await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Category)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Recipe> AddRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe)
        {
            var existingRecipe = await _context.Recipes.FindAsync(id);

            if (existingRecipe == null)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.");
            }

            existingRecipe.Title = recipe.Title;
            existingRecipe.UserId = recipe.UserId;
            existingRecipe.CategoryID = recipe.CategoryID;

            await _context.SaveChangesAsync();

            return existingRecipe;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
