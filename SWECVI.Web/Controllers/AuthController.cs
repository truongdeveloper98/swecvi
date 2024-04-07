using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.Infrastructure;
using SWECVI.Infrastructure.Services;

namespace SWECVI.Web.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IParameterSettingRepository _parameterSettingRepository;


    public AuthController(IAuthenticationService authenticationService,
        IParameterSettingRepository parameterSettingService)
    {
        _authenticationService = authenticationService;
        _parameterSettingRepository = parameterSettingService;
    }
    [Route("api/auth/login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto.Login model, [FromHeader(Name = "HospitalId")] int hospitalId = 2)
    {
        // var models = ExcelExtension.GetValueFromExcel();

        //foreach (var setting in models) {
        //    var parameterSetting = await _parameterSettingRepository.Get(x => x.ParameterId == setting.ParameterId);

        //    if(parameterSetting != null)
        //    {
        //        parameterSetting.ParameterHeaderOrder = setting.ParameterHeaderOrder;
        //        parameterSetting.ParameterHeader = parameterSetting.ParameterHeader;
        //        parameterSetting.ParameterSubHeader = setting.ParameterSubHeader;

        //        await _parameterSettingRepository.Update(setting);
        //    }
        //    else
        //    {
        //        await _parameterSettingRepository.Add(setting);
        //    }
            
        //}

        model.HospitalId = hospitalId;

        var result = await _authenticationService.Login(model);
        
        return Ok(result);
    }
}
