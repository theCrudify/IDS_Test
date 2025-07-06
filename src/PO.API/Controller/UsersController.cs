// FILE LOCATION: src/PO.API/Controllers/UsersController.cs

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing User master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - Users")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<UserDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<UserDto>>>> GetAllUsers()
    {
        _logger.LogInformation("Getting all users");
        var result = await _userService.GetAllUsersAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get users with pagination
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<UserDto>>), 200)]
    public async Task<ActionResult<ApiResult<PagedResult<UserDto>>>> GetPagedUsers([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged users with parameters: {@Request}", request);
        var result = await _userService.GetPagedUsersAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<UserDto>), 200)]
    public async Task<ActionResult<ApiResult<UserDto>>> GetUserById(int id)
    {
        _logger.LogInformation("Getting user by ID: {UserId}", id);
        var result = await _userService.GetUserByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<UserDto>), 201)]
    public async Task<ActionResult<ApiResult<UserDto>>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        _logger.LogInformation("Creating new user with code: {EmployeeCode}", createUserDto.EmployeeCode);
        var result = await _userService.CreateUserAsync(createUserDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<UserDto>), 200)]
    public async Task<ActionResult<ApiResult<UserDto>>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        _logger.LogInformation("Updating user ID: {UserId}", id);
        var result = await _userService.UpdateUserAsync(id, updateUserDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteUser(int id)
    {
        _logger.LogInformation("Deleting user ID: {UserId}", id);
        var result = await _userService.DeleteUserAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get users for lookup/dropdown purposes
    /// </summary>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<UserLookupDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<UserLookupDto>>>> GetUsersForLookup()
    {
        _logger.LogInformation("Getting users for lookup");
        var result = await _userService.GetUsersForLookupAsync();
        return CreateResponse(result);
    }
}
