using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class AssessmentController : ControllerBase
{
    private readonly IAssessmentService _assessmentService;
    public AssessmentController(IAssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }

    [HttpPost]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/assessment-management/assessments")]
    public async Task<IActionResult> Create(AssessmentViewModel model)
    {
        try
        {
           await _assessmentService.Create(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(model);
    }

    [Route("api/assessment-management/assessments/{id}")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _assessmentService.GetById(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/assessment-management/assessments")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> Gets([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
    {
        try
        {
            var result = await _assessmentService.Gets(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/assessment-management/assessments/{id}")]
    public async Task<IActionResult> Updateassessment(int id, [FromBody] AssessmentViewModel model)
    {
        try
        {
            await _assessmentService.Update(id, model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
