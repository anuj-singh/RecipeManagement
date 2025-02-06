using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
       // Task<CommonResponseDto> GetUserByEmailAsync(LoginDto userDtls);
        Task<List<User>> GetAllUserAsync();  
        Task<User> UpdateUserAsync(int id,User user);
        Task<bool> DeleteUserAsync(int id);
    }
}