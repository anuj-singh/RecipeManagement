using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Interfaces;

namespace RecipeManagement.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _recipeRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _recipeRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            try
            {
                return await _recipeRepository.GetAllCategoryAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all categories.", ex);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _recipeRepository.GetCategoryByIdAsync(id);
        }





    }
}