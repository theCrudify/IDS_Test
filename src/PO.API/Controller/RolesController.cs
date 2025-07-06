// FILE LOCATION: src/PO.API/Controllers/RolesController.cs
// DESCRIPTION: Controller for Role master data operations - provides RESTful API endpoints

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing Role master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - Roles")]
public class RolesController : BaseController
{
    private readonly IRoleService _roleService;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IRoleService roleService, ILogger<RolesController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>List of all roles</returns>
    /// <response code="200">Returns the list of roles</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<RoleDto>>), 200)]
    [ProducesResponseType(typeof(ApiResult), 500)]
    public async Task<ActionResult<ApiResult<IEnumerable<RoleDto>>>> GetAllRoles()
    {
        _logger.LogInformation("Getting all roles");
        var result = await _roleService.GetAllRolesAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get roles with pagination
    /// </summary>
    /// <param name="request">Pagination parameters</param>
    /// <returns>Paged list of roles</returns>
    /// <response code="200">Returns the paged list of roles</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<RoleDto>>), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 500)]
    public async Task<ActionResult<ApiResult<PagedResult<RoleDto>>>> GetPagedRoles([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged roles with parameters: {@Request}", request);
        var result = await _roleService.GetPagedRolesAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get role by ID
    /// </summary>
    /// <param name="id">Role ID</param>
    /// <returns>Role details</returns>
    /// <response code="200">Returns the role</response>
    /// <response code="404">Role not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<RoleDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    [ProducesResponseType(typeof(ApiResult), 500)]
    public async Task<ActionResult<ApiResult<RoleDto>>> GetRoleById(int id)
    {
        _logger.LogInformation("Getting role by ID: {RoleId}", id);
        var result = await _roleService.GetRoleByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="createRoleDto">Role creation data</param>
    /// <returns>Created role</returns>
    /// <response code="201">Role created successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="409">Role with same code already exists</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<RoleDto>), 201)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 409)]
    [ProducesResponseType(typeof(ApiResult), 500)]
    public async Task<ActionResult<ApiResult<RoleDto>>> CreateRole([FromBody] CreateRoleDto createRoleDto)
    {
        _logger.LogInformation("Creating new role with code: {RoleCode}", createRoleDto.RoleCode);

        var validationResult = ValidateModelState();
        if (validationResult != null)
            return BadRequest(validationResult);

        var result = await _roleService.CreateRoleAsync(createRoleDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="id">Role ID</param>
    /// <param name="updateRoleDto">Role update data</param>
    /// <returns>Updated role</returns>
    /// <response code="200">Role updated successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="404">Role not found</response>
    /// <response code="409">Role with same code already exists</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<RoleDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    [ProducesResponseType(typeof(ApiResult), 409)]
    [ProducesResponseType(typeof(ApiResult), 500)]
    public async Task<ActionResult<ApiResult<RoleDto>>> UpdateRole(int id, [FromBody] UpdateRoleDto updateRoleDto)
    {
        _logger.LogInformation("Updating role ID: {RoleId}", id);

        var validationResult = ValidateModelState();
        if (validationResult != null)
            return BadRequest(validationResult);

        var result = await _roleService.UpdateRoleAsync(id, updateRoleDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id">Role ID</param>
    /// <returns>Deletion result</returns>
    /// <response code="200">Role deleted successfully</response>
    /// <response code="404">Role not found</response>
    /// <response code="409">Role cannot be deleted (has dependencies)</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    [ProducesResponseType(typeof(ApiResult), 409)]
    [ProducesResponseType(typeof(ApiResult), 500)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteRole(int id)
    {
        _logger.LogInformation("Deleting role ID: {RoleId}", id);
        var result = await _roleService.DeleteRoleAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get roles for lookup/dropdown purposes
    /// </summary>
    /// <returns>Simplified list of roles for selection</returns>
    /// <response code="200">Returns the list of roles for lookup</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<RoleLookupDto>>), 200)]
    [ProducesResponseType(typeof(ApiResult), 500)]
    public async Task<ActionResult<ApiResult<IEnumerable<RoleLookupDto>>>> GetRolesForLookup()
    {
        _logger.LogInformation("Getting roles for lookup");
        var result = await _roleService.GetRolesForLookupAsync();
        return CreateResponse(result);
    }
}