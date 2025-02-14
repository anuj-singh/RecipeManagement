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
        public RecipeService(IRecipeRepository recipeRepository,ILogService logService)
        {
            _recipeRepository = recipeRepository;
            _logService= logService;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            List<Recipe> lstRecipe= new  List<Recipe>();
            try
            {
                lstRecipe= await _recipeRepository.GetAllRecipesAsync();
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"GetAllRecipesAsync");
            }
            return lstRecipe;
        }

        // Get a recipe by its ID
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            Recipe recipe= new  Recipe();
            try
            {
                recipe= await _recipeRepository.GetRecipeByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.", ex);
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"GetRecipeByIdAsync");
            }
            return recipe;
        }

        // Get all recipes by a specific user ID
        public async Task<List<Recipe>> GetAllRecipebyUserIdAsync(int userId)
        {
            List<Recipe> lstRecipe= new  List<Recipe>();
            try
            {
                lstRecipe= await _recipeRepository.GetRecipesByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                 await  _logService.CreateLogAsync(ex.Message,"GetRecipeByIdAsync");
            }
            return lstRecipe;
        }

        // Create a new recipe
        public async Task<CommonResponseDto> CreateRecipeAsync(Recipe recipe)
        {
            CommonResponseDto responseDto= new  CommonResponseDto();
            try
            {
                var recipeData= await _recipeRepository.AddRecipeAsync(recipe);
                if(recipeData!= null)
                {
                    responseDto.Message="Recipe added successfully";
                    responseDto.Status=true;
                }
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
    }
}
