using SWECVI.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.Infrastructure.Services;

namespace SWECVI.Web.Controllers;
[ApiController]
[Authorize]
public class FindingStructureController : ControllerBase
{
    public IFindingStructureService _service;
    public FindingStructureController(IFindingStructureService service)
    {
        _service = service;
    }

    
    [HttpGet]
    [Route("api/finding-structure")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/findingstructure-management/findingstructures")]
    public async Task<IActionResult> Create(FindingStructureViewModel model)
    {
        try
        {
            await _service.Create(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(model);
    }

    [Route("api/findingstructure-management/findingstructures/{id}")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/findingstructure-management/findingstructures")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> Gets([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
    {
        try
        {
            var result = await _service.Gets(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/findingstructure-management/findingstructures/{id}")]
    public async Task<IActionResult> Updatefindingstructure(int id, [FromBody] FindingStructureViewModel model)
    {
        try
        {
            await _service.Update(id, model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}