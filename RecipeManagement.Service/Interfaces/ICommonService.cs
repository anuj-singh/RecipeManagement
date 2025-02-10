using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using  Microsoft.AspNetCore.Http;
namespace RecipeManagement.Service.Interfaces
{
    public interface ICommonService
    {
        Task<bool> UploadPicture(IFormFile formFile);
    
    }
}