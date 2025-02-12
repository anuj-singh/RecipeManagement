using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeManagement.Service.Interfaces
{
    public interface IAuthService
    {
        Task<CommonResponseDto> Register(LoginDto userDtls);
        Task<AuthResponseDto?> Authenticate(LoginDto userDtls);
       // Task<CommonResponseDto> ForgotPassword(LoginDto userDtls);
    }
}