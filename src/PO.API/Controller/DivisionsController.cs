// FILE LOCATION: src/PO.API/Controllers/DivisionsController.cs

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing Division master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - Divisions")]
public class DivisionsController : BaseController
{
    private readonly IDivisionService _divisionService;
    private readonly ILogger<DivisionsController> _logger;

    public DivisionsController(IDivisionService divisionService, ILogger<DivisionsController> logger)
    {
        _divisionService = divisionService;
        _logger = logger;
    }

    /// <summary>
    /// Get all divisions
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<DivisionDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<DivisionDto>>>> GetAllDivisions()
    {
        _logger.LogInformation("Getting all divisions");
        var result = await _divisionService.GetAllDivisionsAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get divisions with pagination
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<DivisionDto>>), 200)]
    public async Task<ActionResult<ApiResult<PagedResult<DivisionDto>>>> GetPagedDivisions([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged divisions with parameters: {@Request}", request);
        var result = await _divisionService.GetPagedDivisionsAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get division by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<DivisionDto>), 200)]
    public async Task<ActionResult<ApiResult<DivisionDto>>> GetDivisionById(int id)
    {
        _logger.LogInformation("Getting division by ID: {DivisionId}", id);
        var result = await _divisionService.GetDivisionByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new division
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<DivisionDto>), 201)]
    public async Task<ActionResult<ApiResult<DivisionDto>>> CreateDivision([FromBody] CreateDivisionDto createDivisionDto)
    {
        _logger.LogInformation("Creating new division with code: {DivisionCode}", createDivisionDto.DivisionCode);
        var result = await _divisionService.CreateDivisionAsync(createDivisionDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing division
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<DivisionDto>), 200)]
    public async Task<ActionResult<ApiResult<DivisionDto>>> UpdateDivision(int id, [FromBody] UpdateDivisionDto updateDivisionDto)
    {
        _logger.LogInformation("Updating division ID: {DivisionId}", id);
        var result = await _divisionService.UpdateDivisionAsync(id, updateDivisionDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete a division
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteDivision(int id)
    {
        _logger.LogInformation("Deleting division ID: {DivisionId}", id);
        var result = await _divisionService.DeleteDivisionAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get divisions for lookup/dropdown purposes
    /// </summary>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<DivisionLookupDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<DivisionLookupDto>>>> GetDivisionsForLookup()
    {
        _logger.LogInformation("Getting divisions for lookup");
        var result = await _divisionService.GetDivisionsForLookupAsync();
        return CreateResponse(result);
    }
}
