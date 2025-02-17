using RecipeManagement.Data;
using RecipeManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecipeManagement.Data.Interfaces;


namespace RecipeManagement.Data.Repositories
{

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly RecipeDBContext _context;

    public PasswordResetTokenRepository(RecipeDBContext context)
    {
        _context = context;
    }

    public async Task SavePasswordResetTokenAsync(int userId, string token, DateTime expirationDate)
    {
        var resetToken = new PasswordResetToken
        {
            UserId = userId,
            Token = token,
            ExpirationDate = expirationDate
        };

        _context.PasswordResetTokens.Add(resetToken);
        await _context.SaveChangesAsync();
    }

    public async Task<int?> GetUserIdByTokenAsync(string token)
    {
        var tokenRecord = await _context.PasswordResetTokens
            .Where(t => t.Token == token && t.ExpirationDate > DateTime.Now)
            .FirstOrDefaultAsync();

        return tokenRecord?.UserId;
    }

    public async Task<bool> IsTokenExpiredAsync(string token)
    {
        var tokenRecord = await _context.PasswordResetTokens
            .Where(t => t.Token == token)
            .FirstOrDefaultAsync();

        return tokenRecord == null || tokenRecord.ExpirationDate < DateTime.Now;
    }

    public async Task DeleteTokenAsync(string token)
    {
        var tokenRecord = await _context.PasswordResetTokens
            .Where(t => t.Token == token)
            .FirstOrDefaultAsync();

        if (tokenRecord != null)
        {
            _context.PasswordResetTokens.Remove(tokenRecord);
            await _context.SaveChangesAsync();
        }
    }
}
}