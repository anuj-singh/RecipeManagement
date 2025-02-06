using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Interfaces;
public class AdminService : IAdminService
    {
       private readonly IUserRepository _userRepository;

    public AdminService(IUserRepository userRepository)
     {
            _userRepository = userRepository;
     }

     // For single user - banned, unbanned
     public async Task<User> BanSingleUserAsync (int id)
    {
       
        try
        {
            if(id != 0)
            {
                    var user = await _userRepository.GetUserById(id);
                    if(user !=null)
                    {
                        user.StatusId = 3;
                        user.UpdatedAt = DateTime.UtcNow;
                        user.LastModifiedUserId = 1;
                        await _userRepository.UpdateUser(id,user);
                         return user;
                        
                    }
                    else{
                        throw new Exception("User not found");
                    }
            }
            else{
                throw new Exception("Invalid user ID");
            }
            
        }

        catch(Exception )
        {
            throw new Exception ("User is not found or already banned");
        }
       
    }

     public async Task<User>  UnBanSingleUserAsync (int id)
    {
        try
        {
            if(id != 0)
            {
                    var user = await _userRepository.GetUserById(id);
                    if(user !=null)
                    {
                        user.StatusId = 1;
                        user.UpdatedAt = DateTime.UtcNow;
                        user.LastModifiedUserId = 1;
                        await _userRepository.UpdateUser(id,user);
                        return user;
                 
                    }
                    else{
                        throw new Exception("User not found");
                    }
            }
            else{
                throw new Exception("Invalid user ID");
            }
            
        }
        catch(Exception )
        {
            throw new Exception ("User is not found or already banned");
        }
    }
    //  For list of user - banned, unbanned
    public async Task<List<User>> BanUserAsync(List<int> userIds)
    {
         var bannedUsers = new List<User>();
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
                         bannedUsers.Add(user);
                    }
                     
                }
               
                
            }
           
            
        }
        catch(Exception )
        {
            throw new Exception ("User is not found or already banned");
        }
         return bannedUsers;
    }

    public async Task<List<User>> UnbanUserAsync(List<int> userIds)
    {
        var UnBannedUsers = new List<User>();
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
                         UnBannedUsers.Add(user);
                    }
                }
            }
        }
        catch(Exception)
        {
             throw new Exception ("User is not found or already unbanned");
        }
        return UnBannedUsers;
    }

    
}