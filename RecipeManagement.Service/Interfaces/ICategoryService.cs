using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CommonResponseDto> CreateCategoryAsync(CategoryDto category);
        Task<CategoryDto?> GetCategoryByIdAsync(int categoryId);   
        Task<List<CategoryDto>> GetAllCategoryAsync();  
        Task<CategoryDto?> UpdateCategoryAsync(int CategoryId,CategoryDto category);
        Task<CommonResponseDto> DeleteCategoryAsync(int CategoryId);
    }
}