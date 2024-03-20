//using Microsoft.AspNetCore.Mvc;
//using SWECVI.ApplicationCore.Interfaces;
//using Microsoft.AspNetCore.Identity;
//using SWECVI.ApplicationCore.Entities;
//using Microsoft.AspNetCore.Authorization;

//namespace SWECVI.Web.Controllers;

//[ApiController]
//public class DicomController : ControllerBase
//{
//    private readonly IDicomService _dicomService;

//    public DicomController(UserManager<AppUser> userManager, IDicomService dicomService)
//    {
//        _dicomService = dicomService;
//    }

//    [HttpGet]
//    [Route("api/dicom-management/force-update")]
//    [Authorize(Roles = "Admin")]
//    public async Task<IActionResult> SyncDataFromDcm4cheServer([FromQuery] bool? today)
//    {
//        await _dicomService.SyncDataFromDcm4cheServer(today);
//        return Ok();
//    }
//}
