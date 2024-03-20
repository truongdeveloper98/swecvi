using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Services;

namespace SWECVI.Web.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<AppUser> _userManager;

    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IUserService userService)
    {
        _userManager = userManager;
        _userService = userService;
    }


    [HttpPost]
    // [Authorize(Roles = "Admin")]
    [Route("api/user-management/users")]
    public async Task<IActionResult> CreateUser([FromBody] UserInformationDto userModel)
    {
        try
        {
            await _userService.CreateUser(userModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut]
    // [Authorize(Roles = "Admin")]
    [Route("api/user-management/users/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserInformationDto userModel)
    {
        try
        {
            await _userService.UpdateUser(id, userModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    [Route("api/user-management/users/{id}/active")]
    public async Task<IActionResult> ActiveUser(int id)
    {
        try
        {
            await _userService.ActiveUser(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("api/user-management/users/{id}/inactive")]
    public async Task<IActionResult> InactiveUser(int id)
    {
        try
        {
            await _userService.InactiveUser(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/user-management/users")]
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
    {
        try
        {
            var result = await _userService.GetUsers(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/user-management/users/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
