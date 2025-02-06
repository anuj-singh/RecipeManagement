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
        public async Task<IActionResult> Register([FromBody]LoginDto user)
        { 
            var response = await _authService.Register(user);
            return Ok(response);
        }
         [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginDto user)
        { 
            var response = await _authService.Authenticate(user);
            return Ok(response);
        }
    }
}