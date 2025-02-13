using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Data.Models;
using BCrypt.Net;

namespace CoreApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            try
            {
                var user = await _UserService.GetAllUsersAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersByIdAsync(int id)
        {
            try
            {
                var user = await _UserService.GetUsersByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = $"User with ID {id} not found." });
                }
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = $"User with ID {id} not found.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving user with ID {id}.", details = ex.Message });
            }
        }


        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Users user)
        {
            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            }

            var createuser = await _UserService.AddUser(user);
            return Ok(createuser);
        }
        //PUT : api/user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users user)
        {
            var updateUser = await _UserService.UpdateUser(id, user);
            if (updateUser == null)
            {
                return NotFound(new { message = $"User with ID {id} not updated." });
            }
            return Ok(updateUser);
        }


        // Delete : api/user

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUser(int id)
        {

            var removeUser = await _UserService.DeleteUser(id);

            if (!removeUser)
            {
                return NotFound(new { message = $"User with ID {id} not deleted." });
            }
            return Ok(removeUser);


        }

        [HttpPost("Login")]
        public async Task<IActionResult> loginUserBysync([FromBody] Login userData)
        {
            var loginUser = await _UserService.loginUserBysync(userData.email, userData.password);
            if (loginUser != null && BCrypt.Net.BCrypt.Verify(userData.password, loginUser.PasswordHash))
            {
                return StatusCode(200, new { status = true, message = $"User is verified", userDetails = loginUser });
            }
            else
            {
                // Email or password is incorrect
                return StatusCode(200, new { status = false, message = $"Email or password is incorrect.." });
            }
        }





    }
}
