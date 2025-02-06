using RecipeManagement.Service.Interfaces;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Dtos;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

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
                if(userExists!= null)
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
                    // UserRole role= new UserRole()
                    // {
                    //     RoleId =3,
                    //     UserId =result.UserId,
                    //     User= result,
                    //     CreatedAt = DateTime.UtcNow,
                    //     CreatedBy=1
                    // };
                    // var usrRole= await _userRoleRepository.CreateUserRole
                    response.Message="User successfully registered";
                    response.Status= true;
                }
           }
           return response;
        }
         public async Task<CommonResponseDto> Authenticate(LoginDto userDtls)
        { 
            CommonResponseDto response= new  CommonResponseDto();
           if(userDtls!= null)
           {
                var userExists= await _userRepository.GetUserByEmaiAsync(userDtls.Email.ToLower());
                if(userExists != null)
                {
                    var decryptPwd= DecryptString(userExists.PasswordHash);
                    if(userDtls.Password== decryptPwd)
                    {
                        response.Message="User validated successfully";
                        response.Status=true;
                    }
                }
                else
                {
                    response.Message="User does not exists";
                }
                
           }
           return response;
        }
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
            catch(Exception ex){
                
            }
            return decrypt;
        }
        public string EncryptString(string strPassword)
        {
            byte[] b= System.Text.ASCIIEncoding.ASCII.GetBytes(strPassword);
            string encryptedString= Convert.ToBase64String(b);
            return encryptedString;
        }
    }
}