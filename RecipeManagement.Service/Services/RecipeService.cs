using RecipeManagement.Service.Interfaces;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Security;

namespace RecipeManagement.Service.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ILogService _logService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        public RecipeService(IRecipeRepository recipeRepository,ILogService logService)
        {
            _recipeRepository = recipeRepository;
            _logService= logService;
        }

        public RecipeService(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, ILogService logService)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _logService = logService;
        }

        private RecipeDTO MapToRecipeDTO(Recipe recipe)
        {
            return new RecipeDTO
            {
                RecipeId = recipe.RecipeId,
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                CookingTime = recipe.CookingTime,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                StatusId = recipe.StatusId,
                CategoryId = recipe.CategoryId,
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                CreatedBy = recipe.CreatedBy,
                LastModifiedUserId = recipe.LastModifiedUserId
            };
        }

        public async Task<List<RecipeDTO>> GetAllRecipesAsync()
        {
            try
            {
                var recipes = await _recipeRepository.GetAllRecipesAsync();
                return recipes.ConvertAll(recipe => MapToRecipeDTO(recipe));
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"GetAllRecipesAsync");
                return new List<RecipeDTO>();
            }
        }

        // Get a recipe by its ID
        public async Task<RecipeDTO> GetRecipeByIdAsync(int id)
        {
            var defaultRecipe= new List<RecipeDTO>();
            try
            {
                var recipe= await _recipeRepository.GetRecipeByIdAsync(id);
                return MapToRecipeDTO(recipe);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.", ex);
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"GetRecipeByIdAsync");
                return null;
            }
        }

        // Get all recipes by a specific user ID
        public async Task<List<RecipeDTO>> GetAllRecipebyUserIdAsync(int userId)
        {
            try
            {
                var recipes = await _recipeRepository.GetRecipesByUserIdAsync(userId);
                return recipes.ConvertAll(recipe => MapToRecipeDTO(recipe));
            }
            catch (Exception ex)
            {
                 await  _logService.CreateLogAsync(ex.Message,"GetRecipeByIdAsync");
                 return new List<RecipeDTO>();
            }
        }

        // Create a new recipe
        public async Task<CommonResponseDto> CreateRecipeAsync(RecipeCreateDto recipeCreateDto)
        {
            CommonResponseDto responseDto= new  CommonResponseDto();
            try
            {
                // Validate CategoryId
                var category = await _categoryRepository.GetCategoryById(recipeCreateDto.CategoryId);
                if (category == null)
                {
                    return new CommonResponseDto
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = $"Category with ID {recipeCreateDto.CategoryId} does not exist."
                    };
                }

                // Validate UserId
                var user = await _userRepository.GetUserById(recipeCreateDto.UserId);
                if (user == null)
                {
                    return new CommonResponseDto
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = $"User with ID {recipeCreateDto.UserId} does not exist."
                    };
                }

                // Map DTO to Entity
                var recipe = new Recipe
                {
                    Title = recipeCreateDto.Title,
                    Description = recipeCreateDto.Description,
                    Ingredients = recipeCreateDto.Ingredients,
                    CookingTime = recipeCreateDto.CookingTime,
                    Instructions = recipeCreateDto.Instructions,
                    ImageUrl = recipeCreateDto.ImageUrl,
                    StatusId = recipeCreateDto.StatusId,
                    CategoryId = recipeCreateDto.CategoryId,
                    UserId = recipeCreateDto.UserId,
                    CreatedAt = DateTime.UtcNow,
                    User = user,
                    Category = category,
                    CreatedBy = user.UserId
                };

                // Add Recipe to Repository
                var createdRecipe = await _recipeRepository.AddRecipeAsync(recipe);

                // Return success response with the created RecipeId
                return new CommonResponseDto
                {
                    Status = true,
                    StatusCode = 201,
                    Message = "Recipe created successfully.",
                    Id = createdRecipe.RecipeId
                };
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"CreateRecipeAsync");
            }
            return responseDto;
        }

        // Update an existing recipe
        public async Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe)
        {
            Recipe recipeData=new Recipe();
            try
            {
                recipeData= await _recipeRepository.UpdateRecipeAsync(id, recipe);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.", ex);
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"UpdateRecipeAsync");
            }
            return recipeData;
        }

        // Delete a recipe by its ID
        public async Task<CommonResponseDto> DeleteRecipeAsync(int id)
        {
            CommonResponseDto responseDto= new CommonResponseDto();
            try
            {
                var result=  await _recipeRepository.DeleteRecipeAsync(id);
                if(result)
                {
                    responseDto.Message="Recipe deleted successfully";
                    responseDto.Status=true;
                }else
                {
                    responseDto.Message="Recipe not deleted";
                    responseDto.Status= false;
                }
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"DeleteRecipeAsync");
            }
            return responseDto;
        }
         public async Task<List< RecipeSearchResultDto>> SearchRecipe(RecipeFilterDto filter){
           List< RecipeSearchResultDto> resultDto= new List<RecipeSearchResultDto>();
           try
            {
                var result=  await _recipeRepository.SearchRecipeByFilter(filter.Title,filter.Ingredients,filter.UserId,filter.CategoryId);
                if(result!= null && result.Count>0)
                {
                    resultDto=  result.Select(s=>new RecipeSearchResultDto
                    {
                        RecipeId=s.RecipeId,
                        Title=s.Title,
                        Ingredients= s.Ingredients,
                        User =new UserDto{
                            UserId=s.User.UserId,
                            UserName=s.User.UserName,
                            Email=s.User.Email
                        },
                        Category=new CategoryDto{
                            CategoryId=s.Category.CategoryId,
                            CategoryName=s.Category.CategoryName
                        }

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"SearchRecipe");
            }
           return resultDto; 
        }

    }
}
