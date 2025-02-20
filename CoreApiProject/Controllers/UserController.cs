using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Service.Interfaces;

namespace CoreApiProject.Controllers;
[Authorize]
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

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        try
        {
            if (userDto == null)
            {
                return BadRequest(new { Message = "Invalid user data." });
            }

            // Create the user and security question
            var user = await _userService.CreateUserAsync(userDto, userDto.SecurityQuestion, userDto.SecurityAnswer);

            return Ok(new { Message = "User created successfully.", User = user });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetUserById(int id) 
    {
        var user =  await _userService.GetUserByIdAsync(id);
        // if(user == null)
        // {
        //     return NotFound();
        // }
        return Ok(user);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userService.GetAllUserAsync();
        return Ok(users);
    }


    [HttpPost("UpdateUserWithImage")]
    public async Task<IActionResult> UpdateUserWithImage(IFormFile file ,int id,[FromForm]UpdateUserDto user)
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
    public async Task<IActionResult> UpdateUser(int id ,UpdateUserDto user)
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
            var updateuser = await _userService.UpdateUserDetailsAsync(id,userDtls);
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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        try
        {
            var token = await _userService.ForgotPasswordAsync(forgotPasswordRequestDto.Email, forgotPasswordRequestDto.SecurityAnswer); // Use security answer validation
            return Ok(new { Message = "Password reset token generated successfully : ", token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        try
        {
            await _userService.ResetPasswordAsync(request.Token, request.NewPassword);
            return Ok(new { Message = "Password has been reset successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("GetUserDetailsForSearch")]
    public async Task<IActionResult> GetUserDetailsForSearch([FromBody]UserFilterDto filter)
    {
        var users = await _userService.SearchUser(filter);
        return Ok(users);
    }
}
