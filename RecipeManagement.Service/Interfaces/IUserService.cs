using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Service.Interfaces
{
    public interface IUserService
    {



        Task<Users> GetUserById(int id);
        Task<Users> AddUser(Users user);
        Task<Users> GetUsersByIdAsync(int id);
        Task<List<Users>> GetAllUsersAsync();
        Task<Users> UpdateUser(int id, Users user);
        Task<bool> DeleteUser(int id);

        Task<Users> loginUserBysync(string email, string password);




    }
}
