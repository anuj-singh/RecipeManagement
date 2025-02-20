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

        public async Task<CommonResponseDto> CreateCategoryAsync(CategoryDto category)
        { 
             CommonResponseDto response= new  CommonResponseDto();
             try{
                var existingCategory = await _categoryRepository.GetCategoryByName(category.CategoryName.ToLower());
                if (existingCategory.Item1 != null && existingCategory.Item2)
                {
                    response.Message = "Category already exists";
                    response.Status = false;
                }
                else
                {
                    Category cateDtls= new Category()
                    {
                        CategoryId=category.CategoryId,
                        CategoryName=category.CategoryName,
                        CreatedAt=DateTime.Now,
                        CreatedBy=1
                    };
                    var createdCategory = await _categoryRepository.CreateCategory(cateDtls);
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
               
            }).ToList();
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"GetAllCategoryAsync");
            }
            return categoryDtos;
        }  
        public async Task<CategoryDto?> UpdateCategoryAsync(int CategoryId,CategoryDto category)
        {
            CategoryDto categoryDto= new CategoryDto();
            try{
                var cateDtls= new Category()
                {
                    CategoryName=category.CategoryName,
                    CategoryId=category.CategoryId
                };
                var updatedCategory = await _categoryRepository.UpdateCategory(CategoryId, cateDtls);
                if (updatedCategory == null)
                {
                    return null;
                }
                categoryDto.CategoryId = updatedCategory.CategoryId;
                categoryDto.CategoryName = updatedCategory.CategoryName;
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