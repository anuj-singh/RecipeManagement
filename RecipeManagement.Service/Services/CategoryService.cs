using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Service.Services
{
    public class CategoryService : ICategoryService
    {
       private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CommonResponseDto> CreateCategoryAsync(Category category)
        { 
             CommonResponseDto response= new  CommonResponseDto();
             var existingCategory = await _categoryRepository.GetCategoryById(category.CategoryId);
             if (existingCategory != null)
            {
                response.Message = "Category already exists";
                response.Status = false;
            }
            else
            {
                var createdCategory = await _categoryRepository.CreateCategory(category);
                response.Message = "Category successfully created";
                response.Status = true;
            }
                
           return response;
        }
        public async Task<CategoryDto?> GetCategoryByIdAsync(int CategoryId)
        {
            var category = await _categoryRepository.GetCategoryById(CategoryId);
            if (category == null)
            {
                return null;
            }
            return new CategoryDto
            {
              CategoryId = category.CategoryId,
              CategoryName = category.CategoryName,
              CreatedAt = category.CreatedAt,
              UpdatedAt = category.UpdatedAt,
              LastModifiedUserId = category.LastModifiedUserId, 
              CreatedBy = category.CreatedBy
            };
        }
        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _categoryRepository.GetAllCategory();
           
            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                LastModifiedUserId = c.LastModifiedUserId,
                CreatedBy = c.CreatedBy
            }).ToList();
        }  
        public async Task<CategoryDto?> UpdateCategoryAsync(int CategoryId,Category category)
        {
            var updatedCategory = await _categoryRepository.UpdateCategory(CategoryId, category);
            if (updatedCategory == null)
            {
                 return null;
            }
            return new CategoryDto
            {
                 CategoryId = updatedCategory.CategoryId,
                 CategoryName = updatedCategory.CategoryName,
                 CreatedAt = updatedCategory.CreatedAt,
                 UpdatedAt = updatedCategory.UpdatedAt,
                 LastModifiedUserId = updatedCategory.LastModifiedUserId,
                 CreatedBy = updatedCategory.CreatedBy
            };
        }
        public async Task<bool> DeleteCategoryAsync(int CategoryId)
        {
            return await _categoryRepository.DeleteCategory(CategoryId);
        }

        }   
}