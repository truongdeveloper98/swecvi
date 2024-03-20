using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class PythonCodeController : ControllerBase
{
    private readonly IPythonCodeService _pythonScriptVersionService;
    public PythonCodeController(IPythonCodeService pythonScriptVersionService)
    {
        _pythonScriptVersionService = pythonScriptVersionService;
    }

    [HttpGet]
    [Authorize(Roles = "HospitalAdmin,SuperAdmin")]
    [Route("api/python-management/python-codes")]
    public async Task<IActionResult> GetPythonCodes()
    {
        try
        {
            var result = await _pythonScriptVersionService.GetPythonCodes();
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "HospitalAdmin,SuperAdmin")]
    [Route("api/python-management/python-codes/{id}")]
    public async Task<IActionResult> CreatePythonCode(int id, [FromBody] PythonCodeVersionDto.CreatePythonCode model)
    {
        try
        {
            await _pythonScriptVersionService.CreatePythonCode(id, model.Script!);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "HospitalAdmin,SuperAdmin")]
    [Route("api/python-management/python-codes/{id}/current-version")]
    public async Task<IActionResult> SetCurrentVersion(int id)
    {
        try
        {
            await _pythonScriptVersionService.SetCurrentVersion(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "HospitalAdmin,SuperAdmin")]
    [Route("api/python-management/python-codes/{id}/reset-default")]
    public async Task<IActionResult> ResetDefault(int id, [FromQuery] bool force = false)
    {
        try
        {
            await _pythonScriptVersionService.ResetDefault(id, force);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize(Roles = "HospitalAdmin,SuperAdmin")]
    [Route("api/python-management/python-codes/{id}")]
    public async Task<IActionResult> DeletePythonCode(int id)
    {
        try
        {
            await _pythonScriptVersionService.DeletePythonCode(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
