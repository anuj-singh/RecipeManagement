using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Data.Interfaces
{
    public interface IUserRepository
    {


        Task<Users> AddUser(Users user);
        Task<Users> loginUserBysync(string email, string Password);

        Task<Users> GetUsersByIdAsync(int id);
        Task<List<Users>> GetAllUsersAsync();
        Task<Users> UpdateUser(int id, Users user);
        Task<bool> DeleteUser(int id);









    }
}