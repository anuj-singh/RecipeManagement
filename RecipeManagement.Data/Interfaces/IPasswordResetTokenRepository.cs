using RecipeManagement.Data.Models;

namespace RecipeManagement.Data.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        Task SavePasswordResetTokenAsync(int userId, string token, DateTime expirationDate);
        Task<int?> GetUserIdByTokenAsync(string token);
        Task<bool> IsTokenExpiredAsync(string token);
        Task DeleteTokenAsync(string token);
    }

}