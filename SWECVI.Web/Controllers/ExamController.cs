//using SWECVI.ApplicationCore.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;

//namespace SWECVI.Web.Controllers;
//[ApiController]
//[Authorize]
//public class ExamController : ControllerBase
//{
//    public IExamService _service;
//    public ExamController(IExamService service)
//    {
//        _service = service;
//    }

//    [HttpGet]
//    [Route("api/exam-management/exam-selectors")]
//    public async Task<IActionResult> GetExamSelectors()
//    {
//        try
//        {
//            var result = await _service.GetExamSelectors();
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/exam-management/exam-numbers")]
//    public async Task<IActionResult> GetExamsByPeriod([FromQuery] string? period)
//    {
//        try
//        {
//            var result = await _service.GetExamsByPeriod(period);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//    [HttpGet]
//    [Route("api/exam-management/exams/{id}/report")]
//    public async Task<IActionResult> GenerateExamReport(int id)
//    {
//        try
//        {
//            var result = await _service.GenerateExamReport(id);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/exam-management/exams/{id}/hl7-measurements")]
//    public async Task<IActionResult> GetHL7MeasurementByExamId(int id)
//    {
//        try
//        {
//            var result = await _service.GetHL7MeasurementByExamId(id);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/exam-management/exam-types")]
//    public async Task<IActionResult> GetExamTypes([FromQuery] string? period)
//    {
//        try
//        {
//            var result = await _service.GetExamTypesByPeriod(period);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/hl7-management/code-meaning-chart")]
//    public async Task<IActionResult> GetCodeMeaningCharts([FromQuery] string[] codeMeaning, [FromQuery] string? period)
//    {
//        try
//        {
//            var result = await _service.GetTextValueByCodeMeaningCharts(codeMeaning, period);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/hl7-management/code-meaning")]
//    public async Task<IActionResult> GetCodeMeanings()
//    {
//        try
//        {
//            var result = await _service.GetCodeMeanings();
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/exam-management/exams/{id}")]
//    public async Task<IActionResult> GetExam(int id)
//    {
//        try
//        {
//            var result = await _service.GetExam(id);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpPut]
//    [Route("api/exampatient-management/exampatients")]
//    [AllowAnonymous]
//    public async Task<IActionResult> GenerateExamPatient()
//    {
//        try
//        {
//            await _service.GenerateExamPatient();
//            return Ok();
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//}