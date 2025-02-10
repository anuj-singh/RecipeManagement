using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Service.Services
{
    public class UserService : IUserService
    {
       private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(UserDto user)
        { 
            User userModel= new User()
            {
                Email=user.Email,
                UserId=user.UserId,
                PasswordHash= user.PasswordHash,
                UserName=user.UserName,
                Bio=user.Bio,
                CreatedAt=DateTime.UtcNow,
                CreatedBy=1,
                ImageUrl=user.ImageUrl,
                StatusId= user.StatusId
            };
            return await _userRepository.CreateUser(userModel);
        }
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var userData = await _userRepository.GetUserById(id);
             if (userData == null)
            {
                return null;
            }
            return new UserDto
            {
                
                Email=userData.Email,
                UserId=userData.UserId,
                PasswordHash= userData.PasswordHash,
                UserName=userData.UserName,
                Bio=userData.Bio,
                CreatedAt=userData.CreatedAt,
                CreatedBy=1,
                ImageUrl=userData.ImageUrl,
                StatusId= userData.StatusId
            };
        }
        public async Task<List<UserDto>> GetAllUserAsync()
        {

            var usersModel = await _userRepository.GetAllUser();

            return usersModel.Select(c => new UserDto
            {
                UserId = c.UserId,
                UserName = c.UserName,
                Email=c.Email,
                ImageUrl=c.ImageUrl,
                Bio=c.Bio,
                PasswordHash= c.PasswordHash,
                StatusId=c.StatusId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                LastModifiedUserId = c.LastModifiedUserId,
                CreatedBy = c.CreatedBy
            }).ToList();
        }  
        public async Task<User> UpdateUserAsync(int id,UserDto user)
        {
            
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
            return await _userRepository.UpdateUser(id,userModel);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUser(id);
        }
        
    }   
}