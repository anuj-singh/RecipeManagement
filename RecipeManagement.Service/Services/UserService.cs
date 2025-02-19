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
        public UserService(IUserRepository userRepository,ICommonService commonService,ILogService logService)
        {
            _userRepository = userRepository;
            _commonService=commonService;
            _logService =logService;
        }

        public async Task<CommonResponseDto> CreateUserAsync(UserDto user)
        {
            CommonResponseDto responseDto = new  CommonResponseDto();
            try{
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
            var userData= await _userRepository.CreateUser(userModel);
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

    }   
}