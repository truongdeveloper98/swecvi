using SWECVI.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SWECVI.ApplicationCore.Interfaces.Services;

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
}