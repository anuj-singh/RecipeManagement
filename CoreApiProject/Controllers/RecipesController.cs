using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreApiProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly ICommonService _commonservice;
        public RecipesController(IRecipeService recipeService,ICommonService commonservice)
        {
            _recipeService = recipeService;
            _commonservice=commonservice;
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            try
            {
                var recipes = await _recipeService.GetAllRecipesAsync();
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving recipes.", details = ex.Message });
            }
        }

        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            try
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(id);
                if (recipe == null)
                {
                    return NotFound(new { message = $"Recipe with ID {id} not found." });
                }
                return Ok(recipe);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = $"Recipe with ID {id} not found.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving recipe with ID {id}.", details = ex.Message });
            }
        }

        // POST: api/recipes
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateDto recipeCreateDto)
        {
            try
            {
                var response = await _recipeService.CreateRecipeAsync(recipeCreateDto);

                if (response.Status)
                {
                    return StatusCode(response.StatusCode, response); // Success response
                }
                else
                {
                    return StatusCode(response.StatusCode, response); // Error response
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CommonResponseDto
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }

        [HttpGet("GetRecipeDetailsForSearch")]
        public async Task<IActionResult> GetRecipeDetailsForSearch(RecipeFilterDto filter)
        {
            var users = await _recipeService.SearchRecipe(filter);
            return Ok(users);
        }

         [HttpPost("CreateRecipeWithImage")]
        public async Task<IActionResult> CreateRecipeWithImage([FromForm]RecipeCreateDto recipeCreateDto,IFormFile file)
        {
            CommonResponseDto response = new CommonResponseDto();
        
            try
            {
                if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

                var isUploaded = await _commonservice.UploadPicture( file);
                if(isUploaded.Item1 )
                {
                        recipeCreateDto.ImageUrl= isUploaded.Item2;
                        var result=  await _recipeService.CreateRecipeAsync(recipeCreateDto);
                        response.Message= "Image successfully uploaded.";
                        response.Status= true;
                }
                else
                {
                    response.Message= "Image not uploaded.";
                    response.Status= false;
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the recipe.", details = ex.Message });
            }
        }

        // PUT: api/recipes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] Recipe recipe)
        {
            try
            {
                var updatedRecipe = await _recipeService.UpdateRecipeAsync(id, recipe);
                if (updatedRecipe == null)
                {
                    return NotFound(new { message = $"Recipe with ID {id} not found for update." });
                }
                return Ok(updatedRecipe);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = $"Recipe with ID {id} not found for update.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating recipe with ID {id}.", details = ex.Message });
            }
        }
         [HttpPut("UpdateRecipeWithImage")]
        public async Task<IActionResult> UpdateRecipeWithImage(int id, [FromForm] Recipe recipe,IFormFile file)
        {
            try
            {
                if (file != null && file.Length != 0)
           {
                var isDeleted=   _commonservice.DeletePicture(file.FileName);
                var result= await _commonservice.UploadPicture( file);
                if(result.Item1) 
                {
                    recipe.ImageUrl= result.Item2;
                }
           }
                var updatedRecipe = await _recipeService.UpdateRecipeAsync(id, recipe);
                if (updatedRecipe == null)
                {
                    return NotFound(new { message = $"Recipe with ID {id} not found for update." });
                }
                return Ok(updatedRecipe);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = $"Recipe with ID {id} not found for update.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating recipe with ID {id}.", details = ex.Message });
            }
        }
    [HttpPost("UploadRecipePic")]
    public async Task<IActionResult> UploadRecipePic(IFormFile file, int id)
    {
        CommonResponseDto response = new CommonResponseDto();
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

       var result= await _commonservice.UploadPicture( file);
       var recipeDtls= await _recipeService.GetRecipeByIdAsync(id);
       if(result.Item1 && recipeDtls!= null)
       {
            var recipeEntity = new Recipe
            {
                RecipeId = recipeDtls.RecipeId,
                Title = recipeDtls.Title,
                Description = recipeDtls.Description,
                Ingredients = recipeDtls.Ingredients,
                CookingTime = recipeDtls.CookingTime,
                Instructions = recipeDtls.Instructions,
                ImageUrl = result.Item2,
                StatusId = recipeDtls.StatusId,
                CategoryId = recipeDtls.CategoryId,
                CreatedAt = recipeDtls.CreatedAt,
                UpdatedAt = recipeDtls.UpdatedAt,
                LastModifiedUserId = recipeDtls.LastModifiedUserId,
                CreatedBy = recipeDtls.CreatedBy
            };
            recipeDtls.ImageUrl= result.Item2;
            var updateuser = await _recipeService.UpdateRecipeAsync(id,recipeEntity);
            response.Message= "Image successfully uploaded.";
            response.Status= true;
         
       }
        else
        {
            response.Message= "Image not uploaded.";
            response.Status= false;
        }
        return Ok(response);
    }
        // DELETE: api/recipes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            try
            {
                var result = await _recipeService.DeleteRecipeAsync(id);
                if (!result.Status)
                {
                    return Ok( result.Message  = $"Recipe with ID {id} not found for deletion." );
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting recipe with ID {id}.", details = ex.Message });
            }
        }
    }
}
