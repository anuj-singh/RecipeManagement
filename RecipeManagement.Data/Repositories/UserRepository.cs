using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using System.Text;

namespace RecipeManagement.Data.Repositories
{
public class UserRepository : IUserRepository
{
    readonly RecipeDBContext _recipeDBContext;

    public UserRepository(RecipeDBContext recipeDBContext)
    {
        _recipeDBContext = recipeDBContext;
    }
    public async Task<User> CreateUser(User user, string securityQuestion, string securityAnswer)
    {
        try
        {
            // First, create the security question for the user
            //var existingQuestion = await _recipeDBContext.SecurityQuestions
                                    //.FirstOrDefaultAsync(sq => sq.Question.ToLower() == securityQuestion.ToLower());                       

            var existingQuestion = await _recipeDBContext.SecurityQuestions
                                    .FirstOrDefaultAsync(sq => sq.Question.ToLower() == securityQuestion.ToLower() && sq.UserId == user.UserId);
                        
            if (existingQuestion == null)
            {
                // If the question doesn't exist, create a new security question
                existingQuestion = new SecurityQuestion
                {
                    Question = securityQuestion,
                    Answer = securityAnswer,
                    UserId = user.UserId
                };

                // Save the security question to the database
                _recipeDBContext.SecurityQuestions.Add(existingQuestion);
                await _recipeDBContext.SaveChangesAsync();
            }
            else
            {
                // If the question exists, validate the answer
                if (!string.Equals(existingQuestion.Answer, securityAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Security answer does not match.");
                }
            }

            // Now, associate the security question with the user
            user.SecurityQuestion = existingQuestion;

            _recipeDBContext.Users.Add(user);
            await _recipeDBContext.SaveChangesAsync(); 
            

            return user;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the user.", ex);
        }
    }
    public async Task<User> GetUserById(int id)
    { 
        var getUser = await _recipeDBContext.Users
                .FirstOrDefaultAsync(r => r.UserId == id);
        if(getUser == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found") ;
        }
        return getUser;
    }
    public async Task<List<User>> GetAllUser()
    {
        var userList=  await _recipeDBContext.Users.ToListAsync();
        return userList;
    }
    public async Task<User> UpdateUser(int id,User user)
    {
       var existinguser = await _recipeDBContext.Users.FindAsync(id);
       if(existinguser == null)
       {
        throw new KeyNotFoundException($"User with ID {id} not found.");
       }

       existinguser.Bio = user.Bio;
       existinguser.Email = user.Email;       
       existinguser.UserName = user.UserName;
       existinguser.ImageUrl= user.ImageUrl;
      // existinguser.PasswordHash=user.PasswordHash;
       existinguser.LastModifiedUserId=1;
       existinguser.UpdatedAt=DateTime.UtcNow;
        existinguser.StatusId= user.StatusId;
       await _recipeDBContext.SaveChangesAsync();
       return user;
    }
    public async Task<bool> DeleteUser(int id)
    { 
         var user = await _recipeDBContext.Users.FindAsync(id);
            if (user != null)
            {
                _recipeDBContext.Users.Remove(user);
                await _recipeDBContext.SaveChangesAsync();
                return true;
            }
            return false;
    }
    public async Task<(User?,bool)> GetUserByEmaiAsync(string Email)
    {
        bool flag = true;
        var getUser = await _recipeDBContext.Users
                .FirstOrDefaultAsync(r => r.Email.ToLower() == Email);
       if(getUser == null)
       {
           flag = false;
       }
        return (getUser,flag);
    }

    public async Task<bool> ValidateSecurityAnswerAsync(string email, string answer)
    {
        try
        {
            var user = await _recipeDBContext.Users
                .Include(u => u.SecurityQuestion)
                .FirstOrDefaultAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null || user.SecurityQuestion == null)
            {
                return false;
            }

            return string.Equals(user.SecurityQuestion.Answer, answer, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while validating the security answer.", ex);
        }
    }

    public string GeneratePasswordResetToken()
    {
        try
        {
            var guid = Guid.NewGuid().ToString();
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(guid));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while generating the password reset token.", ex);
        }
    }

    public async Task SavePasswordResetTokenAsync(int userId, string token, DateTime expirationDate)
    {
        try
        {
            var resetToken = new PasswordResetToken
            {
                UserId = userId,
                Token = token,
                ExpirationDate = expirationDate
            };

            _recipeDBContext.PasswordResetTokens.Add(resetToken);
            await _recipeDBContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while saving the password reset token.", ex);
        }
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        try
        {
            var resetToken = await _recipeDBContext.PasswordResetTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (resetToken == null || resetToken.ExpirationDate < DateTime.Now)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while validating the password reset token.", ex);
        }
    }

    public async Task DeleteTokenAsync(string token)
    {
        try
        {
            var resetToken = await _recipeDBContext.PasswordResetTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (resetToken != null)
            {
                _recipeDBContext.PasswordResetTokens.Remove(resetToken);
                await _recipeDBContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the password reset token.", ex);
        }
    }

    public async Task<SecurityQuestion?> GetSecurityQuestionByUserIdAsync(int userId)
    {
        try
        {
            // Ensure that the database query is awaited first
            var securityQuestion = await _recipeDBContext.SecurityQuestions
                .FirstOrDefaultAsync(sq => sq.UserId == userId);

            // Handle the case where no security question is found
            if (securityQuestion == null)
            {
                throw new InvalidOperationException("Security question not found for the user.");
            }

            return securityQuestion;
        }
        catch (Exception ex)
        {
            // Handle any errors that might occur
            throw new ApplicationException("An error occurred while fetching the security question.", ex);
        }
    }
    
}
}