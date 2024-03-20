using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class ParameterReferenceController : ControllerBase
{
    private readonly IParameterReferenceService _parameterReferenceService;
    public ParameterReferenceController(IParameterReferenceService parameterReferenceService)
    {
        _parameterReferenceService = parameterReferenceService;
    }

    [HttpPost]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/reference-management/references")]
    public async Task<IActionResult> Create(ParameterReferenceViewModel model)
    {
        try
        {
           await _parameterReferenceService.Create(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(model);
    }

    [Route("api/reference-management/references/{id}")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _parameterReferenceService.GetById(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/reference-management/references")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> Gets([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
    {
        try
        {
            var result = await _parameterReferenceService.Gets(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/reference-management/references/{id}")]
    public async Task<IActionResult> Updatereference(int id, [FromBody] ParameterReferenceViewModel model)
    {
        try
        {
            await _parameterReferenceService.Update(id, model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
