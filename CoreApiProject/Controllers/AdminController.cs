using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Data.Models;
using RecipeManagement.Service.Interfaces;

namespace CoreApiProject.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    [HttpPost("BanUser")]
    public async Task<IActionResult> BanUser([FromBody] List<int>userIds)
    { 
        
      var bannedusers = await _adminService.BanUserAsync(userIds);
        return Ok(bannedusers);
    }
    [HttpPost("UnBanUser")]
     public async Task<IActionResult> UnBanUser([FromBody] List<int>userIds)
    { 
       var UnBannedusers = await _adminService.UnbanUserAsync(userIds);
        return Ok(UnBannedusers);
    }
    [HttpPost("BanSingleUser")]
    public async Task<IActionResult> BanSingleUser([FromBody] int id)
    { 
       var BanSingleUser =  await _adminService.BanSingleUserAsync(id);
        return Ok(BanSingleUser);
    }
    [HttpPost("UnBanSingleUser")]
    public async Task<IActionResult> UnBanSingleUser([FromBody] int id)
    { 
       var UnBanSingleUser = await _adminService.UnBanSingleUserAsync(id);
        return Ok(UnBanSingleUser);
    }
}