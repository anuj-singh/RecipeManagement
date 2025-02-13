using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(int CategoryId, Category category);
        Task<bool> DeleteCategory(int CategoryId);
    }
}