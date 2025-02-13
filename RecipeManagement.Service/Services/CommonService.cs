using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using Microsoft.AspNetCore.Http;
using RecipeManagement.Service.Interfaces;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

public class CommonService : ICommonService
 {
    private readonly ILogService _logService;
    public CommonService(ILogService logService)
     {
        _logService =logService;
     }

    public async Task<(bool,string)> UploadPicture(IFormFile file)
    {
        bool result =false;
        string newFileName="";
        try{
            newFileName= GenerateNewFileName(file. FileName);
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            var filePath = Path.Combine(rootPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                result =true;
            }
        }
        catch(Exception ex)
        {
           await _logService.CreateLogAsync(ex.Message,"UploadPicture");
        }
        return (result,newFileName);
    }
    public bool DeletePicture(string name)
    {
        bool result = false;
      var rootPath =  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");  
      var filePath = Path.Combine(rootPath, name);
      try
        {
            if ( File.Exists(filePath))
            {
                File.Delete(filePath);
                result= true;
            }
        }
        catch (Exception ex)
        {
             _logService.CreateLogAsync(ex.Message,"DeletePicture");
        }
        return result;
    }
   public string GenerateNewFileName(string originalFileName)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
        string extension = Path.GetExtension(originalFileName);
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        return $"{fileNameWithoutExtension}_{timestamp}{extension}";
    }
    public string GetCommonPath(string fileName)
    {
        string filePath= "";
       if(fileName != null && fileName!="")
       {
        var rootPath =  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");  
         filePath = Path.Combine(rootPath, fileName);
       }
        return filePath;
    }
    
}