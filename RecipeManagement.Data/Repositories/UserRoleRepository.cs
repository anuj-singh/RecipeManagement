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
        public async Task<Role> GetRoleById(int roleId)
        {
            var getRole = await _recipeDBContext.Roles
                    .FirstOrDefaultAsync(r => r.RoleId == roleId) ?? throw new KeyNotFoundException($"Category with ID {roleId} not found");

            return getRole;
        }
        public async Task<UserRole> GetUserRoleId(int userId)
        {

            var getRoleId = await _recipeDBContext.UserRoles
                    .FirstOrDefaultAsync(r => r.UserId == userId) ?? throw new KeyNotFoundException($"Category with ID {userId} not found");

            return getRoleId;

        }

        public async Task<UserRole> GetUserRoleByStringRole(string roleName)
        {
            var userRole = await _recipeDBContext.UserRoles
                .Include(u => u.Role)
                .FirstOrDefaultAsync(ur => ur.Role.RoleName == roleName) ?? throw new KeyNotFoundException($"UserRole with role {roleName} not found");

            return userRole;
        }



    }
}