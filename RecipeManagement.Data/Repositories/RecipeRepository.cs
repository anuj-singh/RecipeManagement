using Microsoft.EntityFrameworkCore;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDBContext _context;

        public RecipeRepository(RecipeDBContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            try
            {
                return await _context.Recipes
                                     .Include(r => r.User)
                                     .Include(r => r.Category)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the recipes.", ex);
            }
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the recipe with ID {id}.", ex);
            }
        }

        public async Task<List<Recipe>> GetRecipesByUserIdAsync(int userId)
        {
            try
            {
                return await _context.Recipes
                                     .Include(r => r.User)
                                     .Include(r => r.Category)
                                     .Where(r => r.UserId == userId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving recipes for User ID {userId}.", ex);
            }
        }

        public async Task<Recipe> AddRecipeAsync(Recipe recipe)
        {
            try
            {
                var existingRecipe = await _context.Recipes
                                           .FirstOrDefaultAsync(r => r.Title == recipe.Title);

                if (existingRecipe != null)
                {
                    throw new InvalidOperationException($"A recipe with the title '{recipe.Title}' already exists.");
                }

                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return recipe;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the recipe.", ex);
            }
        }

        public async Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe)
        {
            try
            {
                var existingRecipe = await _context.Recipes.FindAsync(id);

                if (existingRecipe == null)
                {
                    throw new KeyNotFoundException($"Recipe with ID {id} not found.");
                }

                existingRecipe.Title = recipe.Title;
                existingRecipe.Ingredients = recipe.Ingredients;
                existingRecipe.Description = recipe.Description;
                existingRecipe.CookingTime = recipe.CookingTime;
                existingRecipe.Instructions = recipe.Instructions;
                existingRecipe.UserId = recipe.UserId;
                existingRecipe.CategoryId = recipe.CategoryId;
                existingRecipe.ImageUrl = recipe.ImageUrl;
                existingRecipe.StatusId = recipe.StatusId;
                existingRecipe.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();

                return existingRecipe;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the recipe with ID {id}.", ex);
            }
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the recipe with ID {id}.", ex);
            }
        }
        public async Task<List<Recipe>> SearchRecipeByFilter(string title, string ingredient, int userId, int categoryId)
        {
            List<Recipe> lstRecipes = new List<Recipe>();
            try
            {
                var result = await _context.Recipes
                                     .Include(r => r.User)
                                     .Include(r => r.Category)
                                     .ToListAsync();
                if (!string.IsNullOrEmpty(title))
                {
                    lstRecipes = result.Where(s => s.Title.Contains(title)).ToList();
                }
                if (!string.IsNullOrEmpty(ingredient))
                {
                    lstRecipes = result.Where(s => s.Ingredients.Contains(ingredient)).ToList();
                }
                if (userId != 0)
                {
                    lstRecipes = result.Where(s => s.UserId == userId).ToList();
                }
                if (categoryId != 0)
                {
                    lstRecipes = result.Where(s => s.CategoryId == categoryId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching the recipe with filters.", ex);
            }
            return lstRecipes;
        }


    }
}
