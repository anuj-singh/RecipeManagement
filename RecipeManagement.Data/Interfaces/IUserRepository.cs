using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Data.Interfaces
{
public interface IUserRepository
{
    Task<User> CreateUser(User user);
    Task<User> GetUserById(int id);
    Task<List<User>> GetAllUser();  
    Task<User> UpdateUser(int id,User user);
    Task<bool> DeleteUser(int id);
}
}