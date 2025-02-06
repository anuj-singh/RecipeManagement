using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Interfaces;

namespace CoreApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]User user)
    { 
        var createuser = await _userService.CreateUserAsync(user);
        return Ok(createuser);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id) 
    {
        var user =  await _userService.GetUserByIdAsync(id);
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userService.GetAllUserAsync();
        return Ok(users);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id,[FromBody]User user)
    {
        var updateuser = await _userService.UpdateUserAsync(id,user);
        if(updateuser == null)
        {
            return NotFound();
        }
        return Ok(updateuser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if(!result)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
