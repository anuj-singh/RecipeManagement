using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;

public interface IAdminService
    {
        Task<CommonResponseDto> BanSingleUserAsync(int id);
        Task <CommonResponseDto>UnBanSingleUserAsync(int id);
        Task <CommonResponseDto> BanUserAsync(List<int> userIds);
        Task <CommonResponseDto>UnbanUserAsync(List<int> userIds);

    }