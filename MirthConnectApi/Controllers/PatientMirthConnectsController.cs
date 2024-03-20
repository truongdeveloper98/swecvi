using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;
using System.Text;

namespace SWECVI.MirthConnectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMirthConnectsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ICacheService _cacheService;

        public PatientMirthConnectsController(IPatientService patientService,
            ICacheService cacheService)
        {
            _patientService = patientService;
            _cacheService = cacheService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var manufacturerDicomParameters = _cacheService.GetManufacturerDicomParameters();
            var dicomTags = _cacheService.GetDicomTags();

            var model = new DicomResultMirthViewModel();

            var hospitalId = Request.Headers["HospitalId"];

            int id = 0;

            int.TryParse(hospitalId, out id);

            try
            {
                string rawContent = string.Empty;
                using (var reader = new StreamReader(Request.Body,
                              encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false))
                {
                    rawContent = await reader.ReadToEndAsync();
                }

                if (!string.IsNullOrEmpty(rawContent))
                {
                    model = JsonConvert.DeserializeObject<DicomResultMirthViewModel>(rawContent);

                    if (model is null)
                    {
                        model = new DicomResultMirthViewModel();
                    }

                    await _patientService.Create(model, id, (List<ApplicationCore.Entities.ManufacturerDicomParameters>)manufacturerDicomParameters, (List<ApplicationCore.Entities.DicomTags>)dicomTags);

                }
                else
                {
                    return BadRequest("No data tranformed form mirth");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }
    }
}
