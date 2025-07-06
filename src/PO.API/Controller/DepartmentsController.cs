// FILE LOCATION: src/PO.API/Controllers/DepartmentsController.cs

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing Department master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - Departments")]
public class DepartmentsController : BaseController
{
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentsController> _logger;

    public DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger)
    {
        _departmentService = departmentService;
        _logger = logger;
    }

    /// <summary>
    /// Get all departments
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<DepartmentDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<DepartmentDto>>>> GetAllDepartments()
    {
        _logger.LogInformation("Getting all departments");
        var result = await _departmentService.GetAllDepartmentsAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get departments with pagination
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<DepartmentDto>>), 200)]
    public async Task<ActionResult<ApiResult<PagedResult<DepartmentDto>>>> GetPagedDepartments([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged departments with parameters: {@Request}", request);
        var result = await _departmentService.GetPagedDepartmentsAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get department by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<DepartmentDto>), 200)]
    public async Task<ActionResult<ApiResult<DepartmentDto>>> GetDepartmentById(int id)
    {
        _logger.LogInformation("Getting department by ID: {DepartmentId}", id);
        var result = await _departmentService.GetDepartmentByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new department
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<DepartmentDto>), 201)]
    public async Task<ActionResult<ApiResult<DepartmentDto>>> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
    {
        _logger.LogInformation("Creating new department with code: {DeptCode}", createDepartmentDto.DeptCode);
        var result = await _departmentService.CreateDepartmentAsync(createDepartmentDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing department
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<DepartmentDto>), 200)]
    public async Task<ActionResult<ApiResult<DepartmentDto>>> UpdateDepartment(int id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
    {
        _logger.LogInformation("Updating department ID: {DepartmentId}", id);
        var result = await _departmentService.UpdateDepartmentAsync(id, updateDepartmentDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete a department
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteDepartment(int id)
    {
        _logger.LogInformation("Deleting department ID: {DepartmentId}", id);
        var result = await _departmentService.DeleteDepartmentAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get departments for lookup/dropdown purposes
    /// </summary>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<DepartmentLookupDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<DepartmentLookupDto>>>> GetDepartmentsForLookup()
    {
        _logger.LogInformation("Getting departments for lookup");
        var result = await _departmentService.GetDepartmentsForLookupAsync();
        return CreateResponse(result);
    }
}
