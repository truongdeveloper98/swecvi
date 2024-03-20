using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels.Hospital;

namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class HospitalController : ControllerBase
{
    private readonly IHospitalService _hospitalService;
    public HospitalController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpPost]
    [Route("api/hospital-management/hospitals")]
    [AllowAnonymous]
    public async Task<IActionResult> Create(HopsitalViewModel model)
    {
        try
        {
           await _hospitalService.CreateHospital(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(model);
    }

    [Route("api/hospital-management/hospitals/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetHospitalById(int id)
    {
        try
        {
            var result = await _hospitalService.GetById(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/hospital-management/hospitals")]
    [HttpGet]
    public async Task<IActionResult> GetHopsitals([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
    {
        try
        {
            var result = await _hospitalService.GetHospitals(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("api/hospital-management/hospitals/{id}")]
    public async Task<IActionResult> UpdateHopsital(int id, [FromBody] HopsitalViewModel HopsitalModel)
    {
        try
        {
            await _hospitalService.UpdateHospital(id, HopsitalModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
