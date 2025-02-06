using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Data.Interfaces
{
public interface IUserRoleRepository
{
    Task<UserRole> CreateUserRole(UserRole usrRole);
    
}
}