using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;

namespace CoreApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
         [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.SecurityQuestion) || string.IsNullOrEmpty(user.SecurityAnswer))
            {
                return BadRequest("Invalid input. Please provide security question and answer.");
            }  
            var response = await _authService.Register(user);
            if (response.Status)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequestDto user)
        {
            var response = await _authService.Authenticate(user);
            if (response == null)
            {
                return Unauthorized(new { Message = "Authentication failed" });
            }
            return Ok(response);
        }
    }
}