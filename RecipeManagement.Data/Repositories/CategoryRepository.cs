using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Repositories
{
public class CategoryRepository : ICategoryRepository
{
    readonly RecipeDBContext _recipeDBContext;

    public CategoryRepository(RecipeDBContext recipeDBContext)
    {
        _recipeDBContext = recipeDBContext;
    }
    public async Task<Category> CreateCategory(Category category)
    { 
        _recipeDBContext.Categories.Add(category);
        await _recipeDBContext.SaveChangesAsync();
        return category;
    }
    public async Task<Category?> GetCategoryById(int CategoryId)
    { 
        var getCategory = await _recipeDBContext.Categories
                .FirstOrDefaultAsync(r => r.CategoryId == CategoryId);
       
        return getCategory;
    }
    public async Task<List<Category>> GetAllCategory()
    {
        var categoryList=  await _recipeDBContext.Categories.ToListAsync();
        return categoryList;
    }
    public async Task<Category> UpdateCategory(int CategoryId,Category category)
    {
       var existingCategory = await _recipeDBContext.Categories.FindAsync(CategoryId);
       if(existingCategory != null)
       {
         existingCategory.CategoryName = category.CategoryName;

       await _recipeDBContext.SaveChangesAsync();
      
       }

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
    public async Task<(Category?,bool)> GetCategoryByName(string categoryName)
    {
        bool flag = true;
        var getCategory = await _recipeDBContext.Categories
                .FirstOrDefaultAsync(r => r.CategoryName.ToLower() == categoryName.ToLower());
       if(getCategory == null)
       {
           flag = false;
       }
        return (getCategory,flag);
    }
       
    }   
}