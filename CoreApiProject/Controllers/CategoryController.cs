using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Interfaces;

namespace CoreApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody]Category category)
    { 
        var response = await _categoryService.CreateCategoryAsync(category);
        return Ok(response);
    }

    [HttpGet("{GetCategoryByIdAsync}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int id) 
    {
        var category =  await _categoryService.GetCategoryByIdAsync(id);
        if(category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoryAsync()
    {
        var users = await _categoryService.GetAllCategoryAsync();
        return Ok(users);
    }

    [HttpPut("{UpdateCategoryAsync}")]
    public async Task<IActionResult> UpdateCategoryAsync(int id,[FromBody]Category category)
    {
        var updatecategory = await _categoryService.UpdateCategoryAsync(id,category);
        if(updatecategory == null)
        {
            return NotFound();
        }
        return Ok(updatecategory);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if(!result)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
