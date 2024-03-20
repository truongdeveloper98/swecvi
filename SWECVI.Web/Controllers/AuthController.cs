using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;
namespace SWECVI.Web.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ICacheService _cacheService;
    private readonly IManufacturerDicomParametersRepository _manufacturerDicomParametersRepository;


    public AuthController(IAuthenticationService authenticationService,
        IManufacturerDicomParametersRepository manufacturerDicomParametersRepository,
        ICacheService cacheService)
    {
        _authenticationService = authenticationService;
        _cacheService = cacheService;
        _manufacturerDicomParametersRepository = manufacturerDicomParametersRepository;
    }
    [Route("api/auth/login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto.Login model, [FromHeader(Name = "HospitalId")] int hospitalId = 2)
    {
        //var dicomTags = _cacheService.GetDicomTags();
        // var models = ExcelExtension.GetValueFromExcel(dicomTags);

        //foreach (var setting in models)
        //{
        //        await _manufacturerDicomParametersRepository.Add(setting);
        //}

        model.HospitalId = hospitalId;

        var result = await _authenticationService.Login(model);
        
        return Ok(result);
    }
}
