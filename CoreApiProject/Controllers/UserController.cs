using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Service.Interfaces;

namespace CoreApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICommonService _commonservice;
    public UserController(IUserService userService,ICommonService commonservice)
    {
        _userService = userService;
        _commonservice= commonservice;
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]UserDto user)
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
    public async Task<IActionResult> UpdateUser(int id,[FromBody]UserDto user,IFormFile file )
    {
         if (file != null && file.Length != 0)
           {
                var result= await _commonservice.UploadPicture( file);
                if(result) 
                {
                    user.ImageUrl= file.FileName;
                }
           }
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
    [HttpPost("UploadUserPic")]
    public async Task<IActionResult> UploadUserPic(IFormFile file, int id)
    {
        CommonResponseDto response = new CommonResponseDto();
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

       var result= await _commonservice.UploadPicture( file);
       var userDtls= await _userService.GetUserByIdAsync(id);
       if(result && userDtls!= null)
       {
            userDtls.ImageUrl= file.FileName;
            var updateuser = await _userService.UpdateUserAsync(id,userDtls);
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
}
