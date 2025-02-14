using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Data.Models;

namespace CoreApiProject.Controllers
{
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
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            try
            {
                var result=  await _recipeService.CreateRecipeAsync(recipe);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the recipe.", details = ex.Message });
            }
        }
         [HttpPost("CreateRecipeWithImage")]
        public async Task<IActionResult> CreateRecipeWithImage([FromForm]Recipe recipe,IFormFile file)
        {
            CommonResponseDto response = new CommonResponseDto();
        
            try
            {
                if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

                var isUploaded = await _commonservice.UploadPicture( file);
                if(isUploaded.Item1 )
                {
                        recipe.ImageUrl= isUploaded.Item2;
                        var result=  await _recipeService.CreateRecipeAsync(recipe);
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
            recipeDtls.ImageUrl= result.Item2;
            var updateuser = await _recipeService.UpdateRecipeAsync(id,recipeDtls);
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
