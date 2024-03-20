//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SWECVI.ApplicationCore.Interfaces.Services;
//using SWECVI.ApplicationCore.ViewModels;

//namespace SWECVI.Web.Controllers;

//[ApiController]
//[Authorize]
//public class ParameterController : ControllerBase
//{
//    private readonly IParameterService _parameterService;
//    public ParameterController(IParameterService parameterService)
//    {
//        _parameterService = parameterService;
//    }
//    [HttpGet]
//    [Route("api/parameters-management/parameter-values")]
//    public async Task<IActionResult> GetValuesByParameters([FromQuery] int ySelectorId, [FromQuery] string xValueSelector, [FromQuery] string? period)
//    {
//        var result = await _parameterService.GetValuesByParameters(ySelectorId, xValueSelector, period);
//        return Ok(result);
//    }

//    [HttpGet]
//    [Route("api/parameters-management/parameter-values-exam")]
//    public async Task<IActionResult> GetValuesByParametersForPatient([FromQuery] int[] ids, [FromQuery] int patientId)
//    {
//        var result = await _parameterService.GetValuesByParametersForPatient(ids, patientId);
//        return Ok(result);
//    }

//    [HttpGet]
//    [Route("api/parameters-management/parameter-names")]
//    public async Task<IActionResult> GetParameterNames()
//    {
//        var result = await _parameterService.GetParameterNames();
//        return Ok(result);
//    }

//    [HttpGet]
//    [Route("api/parameters-management/parameters")]
//    public async Task<IActionResult> GetParameterValues([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
//    {
//        try
//        {
//            var result = await _parameterService.GetParameterValues(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
//            return Ok(result);
//        }
//        catch (Exception e)
//        {
//            return BadRequest(e.Message);
//        }
//    }

//    [HttpPut]
//    [Route("api/parameters-management/parameters/{id}")]
//    public async Task<IActionResult> UpdateParameterValue(int id, ParameterDto.ParameterValue model)
//    {
//        try
//        {
//            await _parameterService.UpdateParameterValue(id, model);
//            return Ok();
//        }
//        catch (Exception e)
//        {
//            return BadRequest(e.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/parameters-management/x-selector")]
//    public async Task<IActionResult> GetXValueSelector()
//    {
//        try
//        {
//            var result = await _parameterService.GetXValueSelector();
//            return Ok(result);
//        }
//        catch (Exception e)
//        {
//            return BadRequest(e.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/parameters-management/function-selectors")]
//    public async Task<IActionResult> GetFunctionNameSelector()
//    {
//        try
//        {
//            var result = await _parameterService.GetFunctionNameSelector();
//            return Ok(result);
//        }
//        catch (Exception e)
//        {
//            return BadRequest(e.Message);
//        }
//    }
//}