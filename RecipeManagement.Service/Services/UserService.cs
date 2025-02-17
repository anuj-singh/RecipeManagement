using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using SQLitePCL;
using System.Runtime.CompilerServices;

namespace RecipeManagement.Service.Services
{
    public class UserService : IUserService
    {
       private readonly IUserRepository _userRepository;
        private readonly ICommonService _commonService;
         private readonly ILogService _logService;
         private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        public UserService(IUserRepository userRepository,ICommonService commonService,ILogService logService, IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _userRepository = userRepository;
            _commonService=commonService;
            _logService =logService;
            _passwordResetTokenRepository = passwordResetTokenRepository;
        }

        public async Task<CommonResponseDto> CreateUserAsync(UserDto userDto, string securityQuestion, string securityAnswer)
        {
            CommonResponseDto responseDto = new  CommonResponseDto();
            try{
            User userModel= new User()
            {
                Email=userDto.Email,
                UserId=userDto.UserId,
                PasswordHash= userDto.PasswordHash,
                UserName=userDto.UserName,
                Bio=userDto.Bio,
                CreatedAt=DateTime.UtcNow,
                CreatedBy=1,
                ImageUrl=userDto.ImageUrl,
                StatusId= userDto.StatusId,
            };
            var userData= await _userRepository.CreateUser(userModel,securityQuestion,securityAnswer);
            if(userData!= null)
            {
                responseDto.Id=userData.UserId;
                responseDto.Message="User successfully created";
                responseDto.Status=true;
            }
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"CreateUserAsync");
            }
            return responseDto;
        }
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            UserDto? userDto = null;
            try{
                var userData = await _userRepository.GetUserById(id);
                if (userData == null)
                {
                    return null;
                }
                userDto= new  UserDto();
                userDto.Email=userData.Email;
                userDto. UserId=userData.UserId;
                userDto.PasswordHash= userData.PasswordHash;
                userDto. UserName=userData.UserName;
                userDto. Bio=userData.Bio;
                userDto.CreatedAt=userData.CreatedAt;
                userDto.CreatedBy=1;
                userDto. ImageUrl=  (userData.ImageUrl!=null && userData.ImageUrl!="" )?_commonService.GetCommonPath( userData.ImageUrl):"";
                userDto. StatusId= userData.StatusId;
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"GetUserByIdAsync");
            }
            return userDto;
        }
        public async Task<List<UserDto>?> GetAllUserAsync()
        {
            List<UserDto> lstUsrDto= new List<UserDto>();
            try{
                var usersModel = await _userRepository.GetAllUser();
                 if (usersModel == null)
                {
                    return null;
                }
                lstUsrDto= usersModel.Select(c => new UserDto
                {
                    UserId = c.UserId,
                    UserName = c.UserName,
                    Email=c.Email,
                    ImageUrl=(c.ImageUrl!=null && c.ImageUrl!="" )?_commonService.GetCommonPath( c.ImageUrl):"",
                    Bio=c.Bio,
                    PasswordHash= c.PasswordHash,
                    StatusId=c.StatusId,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    LastModifiedUserId = c.LastModifiedUserId,
                    CreatedBy = c.CreatedBy
                }).ToList();
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"GetAllUserAsync");
            }
            return lstUsrDto;
        }  
        public async Task<User> UpdateUserAsync(int id,UserDto user)
        {
            User usrdetails= new User();
            try{
            User userModel= new User()
            {
                Email=user.Email,
                UserId=user.UserId,
                PasswordHash= user.PasswordHash,
                UserName=user.UserName,
                Bio=user.Bio,
                CreatedAt=DateTime.UtcNow,
                CreatedBy=user.CreatedBy,
                ImageUrl=user.ImageUrl,
                StatusId= user.StatusId
            };
            usrdetails=  await _userRepository.UpdateUser(id,userModel);
            }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"UpdateUserAsync");
            }
            return usrdetails;
        }
        public async Task<CommonResponseDto> DeleteUserAsync(int id)
        {
            CommonResponseDto responseDto= new CommonResponseDto();
            try{
                var result = await _userRepository.DeleteUser(id);   
                if(result)
                {
                    responseDto.Message="User deleted successfully";
                    responseDto.Status= true;
                }else
                {
                    responseDto.Message="User not deleted";
                    responseDto.Status= false;
                }
             }
            catch(Exception ex)
            {
               await  _logService.CreateLogAsync(ex.Message,"DeleteUserAsync");
            }
            return responseDto;
        }
         public async Task<List< UserSearchResultDto>> SearchUser(UserFilterDto filter){
           List< UserSearchResultDto> resultDto= new List<UserSearchResultDto>();
           try
            {
                var result=  await _userRepository.SearchUserByFilter(filter.UserName,filter.Email,filter.StatusId);
                if(result!= null && result.Count>0)
                {
                    resultDto=  result.Select(s=>new UserSearchResultDto
                    {
                       UserId=s.UserId,
                        UserName=s.UserName,
                        Email= s.Email,
                        StatusName=Enum.GetName(typeof(Status), s.StatusId),
                        Recipes= s.Recipes.Select(r=> new RecipeDTO
                        {
                            RecipeId = r.RecipeId,
                            Title = r.Title,
                            Ingredients = r.Ingredients,
                             CategoryId=r.CategoryId,
                             CookingTime= r.CookingTime,
                             Instructions= r.Instructions
                        }).ToList()
                       
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"Search User Filter");
            }
           return resultDto; 
        }


        public async Task<string> ForgotPasswordAsync(string email, string answer)
        {
            try
            {
                // Step 1: Retrieve user by email
                var (user,found) = await _userRepository.GetUserByEmaiAsync(email);
                if (user == null)
                {
                    throw new InvalidOperationException("User not found.");
                }

                // Step 2: Retrieve the associated security question and validate the answer (case insensitive)
                var securityQuestion = await _userRepository.GetSecurityQuestionByUserIdAsync(user.UserId);
                if (securityQuestion == null)
                {
                    throw new InvalidOperationException("Security question not found.");
                }

                // Validate the answer (case insensitive comparison)
                if (!string.Equals(securityQuestion.Answer, answer, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Incorrect answer to security question.");
                }
                
                // Step 3: Generate a secure password reset token
                var token = Guid.NewGuid().ToString(); // Secure token generation
                var expirationTime = DateTime.Now.AddHours(1); // Token expiration time (1 hour)

                // Step 4: Save the token in the database with the expiration time
                await _passwordResetTokenRepository.SavePasswordResetTokenAsync(user.UserId, token, expirationTime);

                // Step 5: Inform the user (Optional) – You can return the token for frontend to handle
                // Example: Send token to the user (or display on UI for manual entry in next step)
                return token;

            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"ForgotPasswordAsync");
                throw new ApplicationException("An error occurred during password reset process.", ex);
            }
        }

        public async Task ResetPasswordAsync(string token, string newPassword)
        {
            try
            {
                // Step 1: Validate the token
                var userId = await _passwordResetTokenRepository.GetUserIdByTokenAsync(token);
                if (userId == null)
                {
                    throw new InvalidOperationException("Invalid or expired token.");
                }

                // Step 2: Check if the token is expired
                var isTokenExpired = await _passwordResetTokenRepository.IsTokenExpiredAsync(token);
                if (isTokenExpired)
                {
                    throw new InvalidOperationException("Token has expired.");
                }

                // Step 3: Get the user by UserId
                var user = await _userRepository.GetUserById(userId.Value);
                if (user == null)
                {
                    throw new InvalidOperationException("User not found.");
                }

                // Step 4: Hash the new password before saving it
                user.PasswordHash = EncryptString(newPassword);

                // Step 5: Save the new password in the database
                await _userRepository.UpdateUser(user.UserId, user);

                // Step 6: Invalidate the reset token after password reset
                await _passwordResetTokenRepository.DeleteTokenAsync(token);
            }
            catch (Exception ex)
            {
                await  _logService.CreateLogAsync(ex.Message,"ResetPasswordAsync");
                throw new ApplicationException("An error occurred during the password reset.", ex);
            }
        }

        public string EncryptString(string strPassword)
        {
            byte[] b= System.Text.ASCIIEncoding.ASCII.GetBytes(strPassword);
            string encryptedString= Convert.ToBase64String(b);
            return encryptedString;
        }
        
    }   
}