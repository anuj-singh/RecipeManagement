using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Service.Interfaces;

namespace RecipeManagement.Service.Services
{
public class AdminService : IAdminService
    {
       private readonly IUserRepository _userRepository;
       private readonly ILogService _logService; 
       
    public AdminService(IUserRepository userRepository,ILogService logService)
     {
            _userRepository = userRepository;
            _logService=logService;
     }

     // For single user - banned, unbanned
     public async Task<CommonResponseDto> BanSingleUserAsync (int id)
    {
        CommonResponseDto responseDto= new  CommonResponseDto();
        try
        {
            var user = await _userRepository.GetUserById(id);
            if(user !=null)
            {
                user.StatusId = 3;
                user.UpdatedAt = DateTime.UtcNow;
                user.LastModifiedUserId = 1;
                var usrData=  await _userRepository.UpdateUser(id,user);
                if(usrData != null)
                {
                    responseDto.Message="User banned successfully";
                    responseDto.Id=usrData.UserId;
                    responseDto.Status=true;
                }
            }
            else{
                responseDto.Message="User details not found";
                responseDto.Status=false;
            }
        }
        catch(Exception ex)
        {
            await  _logService.CreateLogAsync(ex.Message,"BanSingleUserAsync");
        }
       return responseDto;
    }

     public async Task<CommonResponseDto>  UnBanSingleUserAsync (int id)
    {
        CommonResponseDto responseDto= new CommonResponseDto();
        try
        {
            var user = await _userRepository.GetUserById(id);
            if(user !=null)
            {
                user.StatusId = 1;
                user.UpdatedAt = DateTime.UtcNow;
                user.LastModifiedUserId = 1;
                var usrData= await _userRepository.UpdateUser(id,user);
                if(usrData != null)
                {
                    responseDto.Message="User unbanned successfully";
                    responseDto.Id=usrData.UserId;
                    responseDto.Status=true;
                }
            
            }
            else{
                responseDto.Message="User details not found";
                responseDto.Status=false;
            }
        }
        catch(Exception ex)
        {
            await  _logService.CreateLogAsync(ex.Message,"UnBanSingleUserAsync");
        }
        return responseDto;
    }
    //  For list of user - banned, unbanned
    public async Task<CommonResponseDto> BanUserAsync(List<int> userIds)
    {
        CommonResponseDto responseDto= new CommonResponseDto();
        // var bannedUsers = new List<User>();
        try
        {
            if(userIds != null && userIds.Count> 0)
            {
                foreach(var id in userIds)
                {
                    var user = await _userRepository.GetUserById(id);
                    if(user !=null)
                    {
                        user.StatusId = 3;
                        user.UpdatedAt = DateTime.UtcNow;
                        user.LastModifiedUserId = 1;
                        await _userRepository.UpdateUser(id,user);
                        // bannedUsers.Add(user);
                    }
                }
                responseDto.Message="Users banned successfully";
                responseDto.Status=true;
            }
        }
        catch(Exception ex)
        {
            await  _logService.CreateLogAsync(ex.Message,"BanUserAsync");
        }
         return responseDto;
    }

    public async Task<CommonResponseDto> UnbanUserAsync(List<int> userIds)
    {
        CommonResponseDto responseDto= new CommonResponseDto();
        //var UnBannedUsers = new List<User>();
        try
        {
             if(userIds != null && userIds.Count> 0)
            {
                foreach(var id in userIds)
                {
                    var user = await _userRepository.GetUserById(id);
                    if(user !=null)
                    {
                        user.StatusId = 1;
                        user.UpdatedAt = DateTime.UtcNow;
                        user.LastModifiedUserId = 1;
                        await _userRepository.UpdateUser(id,user);
                        // UnBannedUsers.Add(user);
                    }
                }
                responseDto.Message="Users unbanned successfully";
                responseDto.Status=true;
            }
        }
        catch(Exception ex)
        {
             await  _logService.CreateLogAsync(ex.Message,"UnBanUserAsync");
        }
        return responseDto;
    }
}
}