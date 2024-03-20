using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels.MirthConnect;


namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class StudyFindingController : ControllerBase
{
    private readonly IStudyFindingService _hospitalStudyFindingService;
    public StudyFindingController(IStudyFindingService hospitalStudyFindingService)
    {
        _hospitalStudyFindingService = hospitalStudyFindingService;
    }
    

    [HttpPut]
    [Route("api/study-finding/{hospitalId}")]
    public async Task<IActionResult> UpdateStudyFinding(int hosptalId,StudyFindingViewModel model)
    {
        try
        {
            await _hospitalStudyFindingService.Update(model,hosptalId);
            return Ok(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("api/study-finding")]
    public async Task<IActionResult> CreateStudyFinding(StudyFindingViewModel model)
    {
        try
        {
            await _hospitalStudyFindingService.Create(model, model.HospitalId);
            return Ok(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("api/study-finding/{studyId}")]
    public async Task<IActionResult> GetStudyFindingByStudyId(int studyId)
    {
        try
        {
            var result = await _hospitalStudyFindingService.GetStudyFindingByStudyId(studyId,0);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}