using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserDto user);
        Task<UserDto?> GetUserByIdAsync(int id);
       // Task<CommonResponseDto> GetUserByEmailAsync(LoginDto userDtls);
        Task<List<UserDto>> GetAllUserAsync();  
        Task<User> UpdateUserAsync(int id,UserDto user);
        Task<bool> DeleteUserAsync(int id);
    }
}