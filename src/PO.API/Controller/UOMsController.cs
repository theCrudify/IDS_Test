// FILE LOCATION: src/PO.API/Controllers/UOMsController.cs

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing UOM master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - UOMs")]
public class UOMsController : BaseController
{
    private readonly IUOMService _uomService;
    private readonly ILogger<UOMsController> _logger;

    public UOMsController(IUOMService uomService, ILogger<UOMsController> logger)
    {
        _uomService = uomService;
        _logger = logger;
    }

    /// <summary>
    /// Get all UOMs
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<UOMDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<UOMDto>>>> GetAllUOMs()
    {
        _logger.LogInformation("Getting all UOMs");
        var result = await _uomService.GetAllUOMsAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get UOMs with pagination
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<UOMDto>>), 200)]
    public async Task<ActionResult<ApiResult<PagedResult<UOMDto>>>> GetPagedUOMs([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged UOMs with parameters: {@Request}", request);
        var result = await _uomService.GetPagedUOMsAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get UOM by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<UOMDto>), 200)]
    public async Task<ActionResult<ApiResult<UOMDto>>> GetUOMById(int id)
    {
        _logger.LogInformation("Getting UOM by ID: {UOMId}", id);
        var result = await _uomService.GetUOMByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new UOM
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<UOMDto>), 201)]
    public async Task<ActionResult<ApiResult<UOMDto>>> CreateUOM([FromBody] CreateUOMDto createUOMDto)
    {
        _logger.LogInformation("Creating new UOM with code: {UOMCode}", createUOMDto.UOMCode);
        var result = await _uomService.CreateUOMAsync(createUOMDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing UOM
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<UOMDto>), 200)]
    public async Task<ActionResult<ApiResult<UOMDto>>> UpdateUOM(int id, [FromBody] UpdateUOMDto updateUOMDto)
    {
        _logger.LogInformation("Updating UOM ID: {UOMId}", id);
        var result = await _uomService.UpdateUOMAsync(id, updateUOMDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete a UOM
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteUOM(int id)
    {
        _logger.LogInformation("Deleting UOM ID: {UOMId}", id);
        var result = await _uomService.DeleteUOMAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get UOMs for lookup/dropdown purposes
    /// </summary>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<UOMLookupDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<UOMLookupDto>>>> GetUOMsForLookup()
    {
        _logger.LogInformation("Getting UOMs for lookup");
        var result = await _uomService.GetUOMsForLookupAsync();
        return CreateResponse(result);
    }
}
