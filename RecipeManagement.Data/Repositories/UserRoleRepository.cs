using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Repositories
{
public class UserRoleRepository : IUserRoleRepository
{
    readonly RecipeDBContext _recipeDBContext;

    public UserRoleRepository(RecipeDBContext recipeDBContext)
    {
        _recipeDBContext = recipeDBContext;
    }
    public async Task<UserRole> CreateUserRole(UserRole usrRole)
    { 
        _recipeDBContext.UserRoles.Add(usrRole);
        await _recipeDBContext.SaveChangesAsync();
        return usrRole;
    }
}
}