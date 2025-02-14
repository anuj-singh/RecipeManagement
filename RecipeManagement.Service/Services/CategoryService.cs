using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Service.Services
{
    public class CategoryService : ICategoryService
    {
       private readonly ICategoryRepository _categoryRepository;
        private readonly ILogService _logService;
        public CategoryService(ICategoryRepository categoryRepository,ILogService logService)
        {
            _categoryRepository = categoryRepository;
            _logService=logService;
        }

        public async Task<CommonResponseDto> CreateCategoryAsync(Category category)
        { 
             CommonResponseDto response= new  CommonResponseDto();
             try{
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
             }
             catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"CreateUserAsync");
            }
                
           return response;
        }
        public async Task<CategoryDto?> GetCategoryByIdAsync(int CategoryId)
        {
            CategoryDto categoryDto= new  CategoryDto();
            try{
                var category = await _categoryRepository.GetCategoryById(CategoryId);
                if (category == null)
                {
                    return null;
                }
                categoryDto.CategoryId = category.CategoryId;
                categoryDto. CategoryName = category.CategoryName;
                categoryDto.CreatedAt = category.CreatedAt;
                categoryDto.UpdatedAt = category.UpdatedAt;
                categoryDto. LastModifiedUserId = category.LastModifiedUserId; 
                categoryDto. CreatedBy = category.CreatedBy;
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"GetCategoryByIdAsync");
            }
                return categoryDto;
        }
        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            List<CategoryDto> categoryDtos= new  List<CategoryDto>();
            try{
            var categories = await _categoryRepository.GetAllCategory();
           
            categoryDtos= categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                LastModifiedUserId = c.LastModifiedUserId,
                CreatedBy = c.CreatedBy
            }).ToList();
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"GetAllCategoryAsync");
            }
            return categoryDtos;
        }  
        public async Task<CategoryDto?> UpdateCategoryAsync(int CategoryId,Category category)
        {
            CategoryDto categoryDto= new CategoryDto();
            try{
                var updatedCategory = await _categoryRepository.UpdateCategory(CategoryId, category);
                if (updatedCategory == null)
                {
                    return null;
                }
                categoryDto.CategoryId = updatedCategory.CategoryId;
                categoryDto.CategoryName = updatedCategory.CategoryName;
                categoryDto. CreatedAt = updatedCategory.CreatedAt;
                categoryDto.UpdatedAt = updatedCategory.UpdatedAt;
                categoryDto. LastModifiedUserId = updatedCategory.LastModifiedUserId;
                categoryDto.CreatedBy = updatedCategory.CreatedBy;
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"GetAllCategoryAsync");
            }
            return categoryDto;
        }
        public async Task<CommonResponseDto> DeleteCategoryAsync(int CategoryId)
        {
            CommonResponseDto responseDto= new CommonResponseDto();
            try{
               var result= await _categoryRepository.DeleteCategory(CategoryId);
                if(result)
                {
                    responseDto.Message="Category deleted successfully";
                    responseDto.Status= true;
                }
                else
                {
                    responseDto.Message="Category not deleted";
                    responseDto.Status= true;
                }
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"GetAllCategoryAsync");
            }
            return responseDto;
        }

        }   
}