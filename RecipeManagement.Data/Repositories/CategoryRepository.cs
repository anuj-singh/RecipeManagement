using Microsoft.EntityFrameworkCore;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata;


namespace RecipeManagement.Data.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly RecipeDBContext _recipeDBContext;

        public CategoryRepository(RecipeDBContext recipeDBContext)
        {
            _recipeDBContext = recipeDBContext;
        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            var categoryList = await _recipeDBContext.Categories.ToListAsync();
            return categoryList;
        }


        public async Task<Category> GetCategoryByIdAsync(int id)
        {


            var catId = await _recipeDBContext.Categories
                                                .Where(c => c.CategoryId == id)
                                                .FirstOrDefaultAsync();
            if (catId == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return catId;


        }

        public async Task<Category> CreateCategory(Category category)
        {
            _recipeDBContext.Categories.Add(category);
            await _recipeDBContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategory(int CategoryId, Category category)
        {
            var exCategory = await _recipeDBContext.Categories.FindAsync(CategoryId);
            if (exCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {CategoryId} not found.");
            }
            exCategory.CategoryName = category.CategoryName;

            await _recipeDBContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(int CategoryId)
        {
            var category = await _recipeDBContext.Categories.FindAsync(CategoryId);
            if (category != null)
            {
                _recipeDBContext.Categories.Remove(category);
                await _recipeDBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }



    }


}
