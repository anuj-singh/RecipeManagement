using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Data.Interfaces
{
public interface IUserRepository
{
    Task<User> CreateUser(User user, string securityQuestion, string securityAnswer);
    Task<User> GetUserById(int id);
    Task<List<User>> GetAllUser();  
    Task<User> UpdateUser(int id,User user);
    Task<bool> DeleteUser(int id);
   Task<(User?,bool)> GetUserByEmaiAsync(string Email);
   Task<List<User>> SearchUserByFilter(string userName, string Email,int statusId);

   Task<SecurityQuestion>? GetSecurityQuestionByUserIdAsync(int userId);

}
}