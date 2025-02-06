using RecipeManagement.Data.Models;

public interface IAdminService
    {
        Task<User> BanSingleUserAsync(int id);
        Task <User>UnBanSingleUserAsync(int id);
        Task <List<User>> BanUserAsync(List<int> userIds);
        Task <List<User>>UnbanUserAsync(List<int> userIds);

    }