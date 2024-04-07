using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Elasticsearch;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;
using System.Security.Claims;
using System.Text;

namespace SWECVI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMirthConnectsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPatientElasticSearchService _patientElasticSearchService;
        private readonly ICacheService _cacheService;

        public PatientMirthConnectsController(IPatientService patientService,
            IPatientElasticSearchService patientElasticSearchService,
            ICacheService cacheService)
        {
            _patientService = patientService;
            _patientElasticSearchService = patientElasticSearchService;
            _cacheService = cacheService;
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

                    List<ParameterVaueViewModel> Parameters = new List<ParameterVaueViewModel>();

                    if (!string.IsNullOrEmpty(rawContent))
                    {
                        model = JsonConvert.DeserializeObject<DicomResultMirthViewModel>(rawContent);

                        if (model is null)
                        {
                            model = new DicomResultMirthViewModel();
                        }

                        if (!string.IsNullOrEmpty(model.Tag60051030))
                        {

                            var modelConvert = model.Tag60051030.FromXml<DicomSRViewModel>();
                            if (modelConvert != null)
                            {
                                if (modelConvert.Patient.Study.Series.Parameter.Count() > 0)
                                {
                                    foreach (var x in modelConvert.Patient.Study.Series.Parameter)
                                    {

                                        Parameters.Add(new ParameterVaueViewModel()
                                        {
                                            ParameterId = x.ParameterId,
                                            ResultValue = /*PredicateBuilder.ConvertValue(x.DisplayUnit, x.ResultValue)*/ x.DisplayValue,
                                            ParameterName = x.ParameterName
                                        });
                                    }

                                }
                            }
                        }


                        if (!string.IsNullOrEmpty(model.Tag60051010))
                        {
                            var modelConvert = model.Tag60051010.FromXml<DicomSRMeaViewModel>();
                            if (modelConvert != null)
                            {
                                if (modelConvert.Patient.Exam.MEASUREMENT.Count() > 0)
                                {
                                    modelConvert.Patient.Exam.MEASUREMENT.ForEach(x => Parameters.Add(new ParameterVaueViewModel()
                                    {
                                        ParameterId = x.parameterId,
                                        ResultValue = /*PredicateBuilder.ConvertValue(x.displayUnit, x.valueDouble)*/ x.valueDouble.ToString(),
                                        ParameterName = x.parameterName
                                    }));
                                }
                            }
                        }

                        await _patientService.Create(model, id, (List<ApplicationCore.Entities.ManufacturerDicomParameters>)manufacturerDicomParameters, (List<ApplicationCore.Entities.DicomTags>)dicomTags, Parameters);

                    }
                    else
                    {
                        return BadRequest("No data tranformed form mirth");
                    }

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
