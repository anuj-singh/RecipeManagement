using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Dtos;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Models;

namespace RecipeManagement.Service.Services
{
    public class LogService : ILogService
    {
       private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<bool> CreateLogAsync(string strException,string funName)
        {
            Log logData= new  Log();
            logData.ErrorInformation=strException;
            logData.CreatedAt= DateTime.UtcNow;
            logData.FunctionName=funName;

            var result = await _logRepository.Createlog(logData);
            return result;
        }
    }   
}