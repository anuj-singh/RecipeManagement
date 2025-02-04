using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Data.Models;

namespace CoreApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
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
                var createdRecipe = await _recipeService.CreateRecipeAsync(recipe);
                return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipe.RecipeId }, createdRecipe);
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

        // DELETE: api/recipes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            try
            {
                var result = await _recipeService.DeleteRecipeAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Recipe with ID {id} not found for deletion." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting recipe with ID {id}.", details = ex.Message });
            }
        }
    }
}
