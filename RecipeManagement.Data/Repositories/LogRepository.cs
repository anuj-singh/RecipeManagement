using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Repositories
{
public class LogRepository : ILogRepository
{
    readonly RecipeDBContext _recipeDBContext;

    public LogRepository(RecipeDBContext recipeDBContext)
    {
        _recipeDBContext = recipeDBContext;
    }

        public async Task<bool> Createlog(Log log)
        {
             _recipeDBContext.Logs.Add(log);
            await _recipeDBContext.SaveChangesAsync();
            return true;
        }
    }
}