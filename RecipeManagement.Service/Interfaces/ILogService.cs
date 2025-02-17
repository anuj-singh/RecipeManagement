using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Service.Interfaces
{
    public interface ILogService
    {
        Task<bool> CreateLogAsync(string strException, string funName);
        
    }
}