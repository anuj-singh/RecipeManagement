using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Repositories
{
public class UserRepository : IUserRepository
{
    readonly RecipeDBContext _recipeDBContext;

    public UserRepository(RecipeDBContext recipeDBContext)
    {
        _recipeDBContext = recipeDBContext;
    }
    public async Task<User> CreateUser(User user)
    { 
        _recipeDBContext.Users.Add(user);
        await _recipeDBContext.SaveChangesAsync();
        return user;
    }
    public async Task<User> GetUserById(int id)
    { 
        var getUser = await _recipeDBContext.Users
                .FirstOrDefaultAsync(r => r.UserId == id);
        if(getUser == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found") ;
        }
        return getUser;
    }
    public async Task<List<User>> GetAllUser()
    {
        var userList=  await _recipeDBContext.Users.ToListAsync();
        return userList;
    }
    public async Task<User> UpdateUser(int id,User user)
    {
       var existinguser = await _recipeDBContext.Users.FindAsync(id);
       if(existinguser == null)
       {
        throw new KeyNotFoundException($"User with ID {id} not found.");
       }

       existinguser.Bio = user.Bio;
       existinguser.Email = user.Email;       
       existinguser.UserName = user.UserName;

       await _recipeDBContext.SaveChangesAsync();
       return user;
    }
    public async Task<bool> DeleteUser(int id)
    { 
         var user = await _recipeDBContext.Users.FindAsync(id);
            if (user != null)
            {
                _recipeDBContext.Users.Remove(user);
                await _recipeDBContext.SaveChangesAsync();
                return true;
            }
            return false;
    }
    public async Task<User> GetUserByEmaiAsync(string Email)
    {
        var getUser = await _recipeDBContext.Users
                .FirstOrDefaultAsync(r => r.Email.ToLower() == Email);
       if(getUser == null)
       {
        throw new KeyNotFoundException($"User with email {Email} not found.");
       }
        return getUser;
    }
}
}