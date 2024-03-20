//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SWECVI.ApplicationCore.Interfaces.Services;
//using SWECVI.ApplicationCore.ViewModels;

//namespace SWECVI.Web.Controllers;

//[ApiController]
//[Authorize]
//public class SessionFieldController : ControllerBase
//{
//    private readonly ISessionFieldService _sessionFieldService;

//    public SessionFieldController(ISessionFieldService sessionFieldService)
//    {
//        _sessionFieldService = sessionFieldService;
//    }

//    [HttpPost]
//    [Authorize(Roles = "Admin")]
//    [Route("api/field-management/fields")]
//    public async Task<IActionResult> CreateSessionField([FromBody] SessionDto.SessionField model)
//    {
//        try
//        {
//            await _sessionFieldService.CreateSessionField(model);
//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//    [HttpPut]
//    [Authorize(Roles = "Admin")]
//    [Route("api/field-management/fields/{id}")]
//    public async Task<IActionResult> UpdateSessionField(int id, [FromBody] SessionDto.SessionField model)
//    {
//        try
//        {
//            await _sessionFieldService.UpdateSessionField(id, model);
//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpDelete]
//    [Authorize(Roles = "Admin")]
//    [Route("api/field-management/fields/{id}")]
//    public async Task<IActionResult> DeleteSessionField(int id)
//    {
//        try
//        {
//            await _sessionFieldService.DeleteSessionField(id);
//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//}
