using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Data.Interfaces
{
public interface ICategoryRepository
{
    Task<Category> CreateCategory(Category category);
    Task<Category?> GetCategoryById(int CategoryId);
    Task<List<Category>> GetAllCategory();  
    Task<Category> UpdateCategory(int CategoryId,Category user);
    Task<bool> DeleteCategory(int CategoryId);
    Task<(Category?,bool)> GetCategoryByName(string categoryName);

}
}