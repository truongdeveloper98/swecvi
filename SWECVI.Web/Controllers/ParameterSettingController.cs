using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class ParameterSettingController : ControllerBase
{
    private readonly IParameterSettingService _parameterSettingService;
    public ParameterSettingController(IParameterSettingService parameterSettingService)
    {
        _parameterSettingService = parameterSettingService;
    }

    [HttpPost]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/parametersetting-management/parametersettings")]
    public async Task<IActionResult> Create(ParameterSettingViewModel model)
    {
        try
        {
           await _parameterSettingService.Create(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(model);
    }

    [Route("api/parametersetting-management/parametersettings/{id}")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _parameterSettingService.GetById(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/parametersetting-management/parametersettings")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> Gets([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
    {
        try
        {
            var result = await _parameterSettingService.Gets(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/parametersetting-management/parametersettings/{id}")]
    public async Task<IActionResult> Updateparametersetting(int id, [FromBody] ParameterSettingViewModel model)
    {
        try
        {
            await _parameterSettingService.Update(id, model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
