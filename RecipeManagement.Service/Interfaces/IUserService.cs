using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Service.Interfaces
{
    public interface IUserService
    {
        Task<CommonResponseDto> CreateUserAsync(UserDto user, string securityQuestion, string securityAnswer);
        Task<UserDto?> GetUserByIdAsync(int id);
       // Task<CommonResponseDto> GetUserByEmailAsync(LoginDto userDtls);
        Task<List<UserDto>?> GetAllUserAsync();  
        Task<User> UpdateUserAsync(int id,UserDto user);
        Task<CommonResponseDto> DeleteUserAsync(int id);
         Task<List<UserSearchResultDto>> SearchUser(UserFilterDto filter);

        Task<string> ForgotPasswordAsync(string email, string securityAnswer);
        Task ResetPasswordAsync(string token, string newPassword);
    }
}