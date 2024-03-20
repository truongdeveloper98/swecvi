using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using System.Security.Claims;

namespace SWECVI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMirthConnectsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientMirthConnectsController(IPatientService patientService)
        {
            _patientService = patientService;
        }


        [HttpGet]
        [Route("patients")]
        public async Task<IActionResult> GetPatients([FromQuery] string? dicomPatientId, [FromQuery] string? period, [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
        {
            try
            {
                int hospitalId = 0;

                var id = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

                int.TryParse(id, out hospitalId);

               

                var result = await _patientService.GetPatients(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch, dicomPatientId, period, hospitalId, role);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("patients/{hospitalId}/{id}/exams")]
        public async Task<IActionResult> GetExamsByPatientId(int hospitalId, int id, [FromQuery] string? period)
        {
            try
            {
                var result = await _patientService.GetExamsByPatientId(id, period, hospitalId);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      }
}
