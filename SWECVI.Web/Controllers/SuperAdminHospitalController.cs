using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;

namespace SWECVI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminHospitalController : ControllerBase
    {
        private readonly IHospitalService _superAdminHospitalService;
        public SuperAdminHospitalController(IHospitalService superAdminHospitalService)
        {
            _superAdminHospitalService = superAdminHospitalService;
        }


        [HttpGet]
        [Route("hospitals")]
        public async Task<IActionResult> GetHospitals()
        {
            try
            {
                var result = await _superAdminHospitalService.GetHospitals();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("hospitals/{id}/")]
        public async Task<IActionResult> GetHospitalById(int id)
        {
            try
            {
                var result = await _superAdminHospitalService.GetHospitalById(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
