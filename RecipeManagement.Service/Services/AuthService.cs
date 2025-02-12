using RecipeManagement.Service.Interfaces;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RecipeManagement.Service.Services
{
    public class AuthService : IAuthService
    {
       private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public AuthService(IUserRepository userRepository,IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository= userRoleRepository;
        }
        public  async Task<CommonResponseDto> Register(LoginDto userDtls)
        { 
            CommonResponseDto response= new  CommonResponseDto();
           if(userDtls!= null)
           {
                var userExists= await _userRepository.GetUserByEmaiAsync(userDtls.Email.ToLower());
                if(userExists.Item1 != null && userExists.Item2 == true)
                {
                    response.Message="User already exists";
                }
                else
                {
                    //string passwordSalt= GetRandomSalt();
                    //string passwordHash=PasswordHash(userDtls.Password,passwordSalt);
                    string passwordEncrypt= EncryptString(userDtls.Password);
                    User userData= new  User()
                    {
                        Email= userDtls.Email,
                        UserName= userDtls.UserName,
                        PasswordHash=passwordEncrypt,
                        CreatedAt= DateTime.UtcNow,
                        CreatedBy= 1,
                        StatusId=1
                    };
                    var result= await _userRepository.CreateUser(userData);
                    var roleDtls= await _userRoleRepository.GetRoleById(3);
                    UserRole role= new UserRole()
                    {
                        RoleId =3,
                        UserId =result.UserId,
                        User= result,
                        Role=roleDtls,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy=1
                    };
                    var usrRole= await _userRoleRepository.CreateUserRole(role);
                    response.Message="User successfully registered";
                    response.Status= true;
                }
           }
           return response;
        }
        //  public async Task<CommonResponseDto> Authenticate(LoginDto userDtls)
        // { 
        //     CommonResponseDto response= new  CommonResponseDto();
        //    if(userDtls!= null)
        //    {
        //         var userExists= await _userRepository.GetUserByEmaiAsync(userDtls.Email.ToLower());
        //         if(userExists != null)
        //         {
        //             var decryptPwd= DecryptString(userExists.PasswordHash);
        //             if(userDtls.Password== decryptPwd)
        //             {
        //                 response.Message="User validated successfully";
        //                 response.Status=true;

        //             }

        //         }
        //         else
        //         {
        //             response.Message="User does not exists";
        //         }
                
        //    }
        //    return response;
        // }
        //  public  async Task<CommonResponseDto> ForgotPassword(LoginDto userDtls)
        // { 
        //     CommonResponseDto response = new  CommonResponseDto();
        
        //     return   response;
        // }
        
        //public string GetRandomSalt()=>BCrypt.Net.BCrypt.GenerateSalt(12);
       // public string PasswordHash(string password,string passwordSalt)=>BCrypt.Net.BCrypt.HashPassword(password,passwordSalt);
       // public bool ValidatePassword(string password,string correctHash)=> BCrypt.Net.BCrypt.Verify(password,correctHash);
        // public string DecryptPassword(string text)
        // {
            
        // }
        public string DecryptString(string password)
        {
            byte[] b;
            string decrypt="";
            try{
                b= Convert.FromBase64String(password);
               decrypt=System.Text.ASCIIEncoding.ASCII.GetString(b);
                
            }
            catch(Exception ex)
            {
               throw ex;
            }
   
            return decrypt;
        }
        public string EncryptString(string strPassword)
        {
            
            byte[] b= System.Text.ASCIIEncoding.ASCII.GetBytes(strPassword);
            string encryptedString= Convert.ToBase64String(b);
            return encryptedString;
        }

     public async Task<AuthResponseDto?> Authenticate(LoginDto userDtls)
        {
            if (userDtls != null)
            {
                var userExists = await _userRepository.GetUserByEmaiAsync(userDtls.Email.ToLower());
                if (userExists.Item1 != null && userExists.Item2 )
                {  
                    var decryptPwd = DecryptString(userExists.Item1.PasswordHash);
                    if (userDtls.Password == decryptPwd)
                    {
                        var roleDtls = await _userRoleRepository.GetUserRoleId(userExists.Item1.UserId);
                        var role = await _userRoleRepository.GetRoleById(roleDtls.RoleId);
                        var response = GenerateToken(userExists.Item1, role);
                    
                        return response;
                       
                    }
                }
            }
            return null; 
        }

    private AuthResponseDto GenerateToken(User user, Role role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyString = "your-256-bit-secret-key-which-is-32-bytes-long";
            var key = Encoding.UTF8.GetBytes(keyString);

            var claims = new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role.RoleName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

             var token = tokenHandler.CreateToken(tokenDescriptor);
             var tokenString = tokenHandler.WriteToken(token);
             var refreshToken = Guid.NewGuid().ToString();

             return new AuthResponseDto
             {
                UserId = user.UserId.ToString(),
                UserName = user.UserName,
                Email = user.Email,
                Role = role.RoleName,
                Token = tokenString,
                RefreshToken = refreshToken
            };
        }       


}
}