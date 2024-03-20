using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpDelete]
    [Route("api/patient-management/patients/{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        try
        {
            await _patientService.Delete(id);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("api/patient-management/patients/")]
    [AllowAnonymous]
    public async Task<IActionResult> ExportData([FromBody]ExportParameterRequest request)
    {
        try
        {
            var stream = await _patientService.ExportData(request.StartDate, request.EndDate, request.PatientId, request.StudyId);

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ParameterResult");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
