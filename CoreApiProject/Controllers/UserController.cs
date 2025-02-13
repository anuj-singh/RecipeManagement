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
    // [HttpPost("CreateUser")]
    // public async Task<IActionResult> CreateUser([FromBody]UserDto user)
    // { 
    //     var createuser = await _userService.CreateUserAsync(user);
    //     return Ok(createuser);
    // }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetUserById(int id) 
    {
        var user =  await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userService.GetAllUserAsync();
        return Ok(users);
    }

    [HttpPost("UpdateUserWithImage")]
    public async Task<IActionResult> UpdateUserWithImage(IFormFile file ,int id,[FromForm]UserDto user)
    {
         if (file != null && file.Length != 0)
           {
                var isDeleted=   _commonservice.DeletePicture(file.FileName);
                var result= await _commonservice.UploadPicture( file);
                if(result.Item1) 
                {
                    user.ImageUrl= result.Item2;
                }
           }
        var updateuser = await _userService.UpdateUserAsync(id,user);
        return Ok(updateuser);
    }
 [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(int id ,UserDto user)
    {
        var updateuser = await _userService.UpdateUserAsync(id,user);
        
        return Ok(updateuser);
    }
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
       
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
       if(result.Item1 && userDtls!= null)
       {
            userDtls.ImageUrl= result.Item2;
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
