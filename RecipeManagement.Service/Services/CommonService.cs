using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;
using Microsoft.AspNetCore.Http;
using RecipeManagement.Service.Interfaces;

public class CommonService : ICommonService
 {
    public CommonService()
     {
     }

    public async Task<bool> UploadPicture(IFormFile file)
    {
        bool result =false;
        string fileName= file.FileName;
         var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        var filePath = Path.Combine(rootPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
            result =true;
        }
        return result;
    }
}