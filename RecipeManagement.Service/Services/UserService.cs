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



        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<Users> AddUser(Users user)
        {
            return await _userRepository.AddUser(user);
        }

        public async Task<Users> loginUserBysync(string email, string password)
        {
            return await _userRepository.loginUserBysync(email, password);
        }
        public async Task<Users> GetUsersByIdAsync(int id)
        {
            return await _userRepository.GetUsersByIdAsync(id);
        }
        public async Task<Users> UpdateUser(int id, Users user)
        {
            return await _userRepository.UpdateUser(id, user);
        }
        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public Task<Users> GetUserById(int id)
        {
            throw new NotImplementedException();
        }


    }
}