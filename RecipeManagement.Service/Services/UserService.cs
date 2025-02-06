using RecipeManagement.Service.Interfaces;
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

        public async Task<User> CreateUserAsync(User user)
        { 
            return await _userRepository.CreateUser(user);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        public async Task<List<User>> GetAllUserAsync()
        {
            return await _userRepository.GetAllUser();
        }  
        public async Task<User> UpdateUserAsync(int id,User user)
        {
            return await _userRepository.UpdateUser(id,user);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}