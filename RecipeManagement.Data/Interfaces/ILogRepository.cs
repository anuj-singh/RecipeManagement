using RecipeManagement.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Data.Interfaces
{
public interface ILogRepository
{
    Task<bool> Createlog(Log log);
}
}