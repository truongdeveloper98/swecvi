using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels.Hospital;

namespace SWECVI.Web.Controllers;

[ApiController]
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpPost]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/department-management/departments")]
    public async Task<IActionResult> Create(DepartmentViewModel model)
    {
        try
        {
           await _departmentService.CreateDepartment(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(model);
    }

    [Route("api/department-management/departments/{id}")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        try
        {
            var result = await _departmentService.GetById(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("api/department-management/departments")]
    [Authorize(Roles = "HospitalAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetDepartments([FromQuery] int currentPage = 0, [FromQuery] int pageSize = 10, [FromQuery] string? sortColumnDirection = "DESC", [FromQuery] string? sortColumnName = "", [FromQuery] string? textSearch = "")
    {
        try
        {
            var result = await _departmentService.GetDepartments(currentPage, pageSize, sortColumnDirection, sortColumnName, textSearch);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "HospitalAdmin")]
    [Route("api/department-management/departments/{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentViewModel model)
    {
        try
        {
            await _departmentService.UpdateDepartment(id, model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
