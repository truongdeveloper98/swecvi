//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SWECVI.ApplicationCore.Interfaces.Services;
//using SWECVI.ApplicationCore.ViewModels;

//namespace SWECVI.Web.Controllers;

//[ApiController]
//[Authorize]
//public class SessionController : ControllerBase
//{
//    private readonly ISessionService _sessionService;
//    private readonly ISessionFieldService _sessionFieldService;


//    public SessionController(ISessionService sessionService, ISessionFieldService sessionFieldService)
//    {
//        _sessionService = sessionService;
//        _sessionFieldService = sessionFieldService;
//    }

//    [HttpGet]
//    [Route("api/session-management/sessions")]
//    public async Task<IActionResult> GetSessions([FromQuery] string[]? includes)
//    {
//        try
//        {
//            var result = await _sessionService.GetSessions(includes);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpGet]
//    [Route("api/session-management/sessions/{id}")]

//    public async Task<IActionResult> GetSessionById(int id)
//    {
//        try
//        {
//            var result = await _sessionService.GetSessionById(id);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpPost]
//    [Authorize(Roles = "Admin")]
//    [Route("api/session-management/sessions")]

//    public async Task<IActionResult> CreateSession([FromBody] SessionDto.Session model)
//    {
//        try
//        {
//            await _sessionService.CreateSession(model);
//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//    [HttpPut]
//    [Authorize(Roles = "Admin")]
//    [Route("api/session-management/sessions/{id}")]

//    public async Task<IActionResult> UpdateSession(int id, [FromBody] SessionDto.Session model)
//    {
//        try
//        {
//            await _sessionService.UpdateSession(id, model);
//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    [HttpDelete]
//    [Authorize(Roles = "Admin")]
//    [Route("api/session-management/sessions/{id}")]
//    public async Task<IActionResult> DeleteSession(int id)
//    {
//        try
//        {
//            await _sessionService.DeleteSession(id);
//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    // session field apis
//    [HttpGet]
//    [Route("api/session-management/sessions/{sessionId}/fields")]
//    public async Task<IActionResult> GetSessionFieldsBySessionId(int sessionId)
//    {
//        try
//        {
//            var result = await _sessionFieldService.GetSessionFieldsBySessionId(sessionId);
//            return Ok(result);
//        }
//        catch (System.Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//}
