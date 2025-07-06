// FILE LOCATION: src/PO.API/Controllers/TaxesController.cs

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing Tax master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - Taxes")]
public class TaxesController : BaseController
{
    private readonly ITaxService _taxService;
    private readonly ILogger<TaxesController> _logger;

    public TaxesController(ITaxService taxService, ILogger<TaxesController> logger)
    {
        _taxService = taxService;
        _logger = logger;
    }

    /// <summary>
    /// Get all taxes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<TaxDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<TaxDto>>>> GetAllTaxes()
    {
        _logger.LogInformation("Getting all taxes");
        var result = await _taxService.GetAllTaxesAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get taxes with pagination
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<TaxDto>>), 200)]
    public async Task<ActionResult<ApiResult<PagedResult<TaxDto>>>> GetPagedTaxes([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged taxes with parameters: {@Request}", request);
        var result = await _taxService.GetPagedTaxesAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get tax by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<TaxDto>), 200)]
    public async Task<ActionResult<ApiResult<TaxDto>>> GetTaxById(int id)
    {
        _logger.LogInformation("Getting tax by ID: {TaxId}", id);
        var result = await _taxService.GetTaxByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new tax
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<TaxDto>), 201)]
    public async Task<ActionResult<ApiResult<TaxDto>>> CreateTax([FromBody] CreateTaxDto createTaxDto)
    {
        _logger.LogInformation("Creating new tax with code: {TaxCode}", createTaxDto.TaxCode);
        var result = await _taxService.CreateTaxAsync(createTaxDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing tax
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<TaxDto>), 200)]
    public async Task<ActionResult<ApiResult<TaxDto>>> UpdateTax(int id, [FromBody] UpdateTaxDto updateTaxDto)
    {
        _logger.LogInformation("Updating tax ID: {TaxId}", id);
        var result = await _taxService.UpdateTaxAsync(id, updateTaxDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete a tax
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteTax(int id)
    {
        _logger.LogInformation("Deleting tax ID: {TaxId}", id);
        var result = await _taxService.DeleteTaxAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get taxes for lookup/dropdown purposes
    /// </summary>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<TaxLookupDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<TaxLookupDto>>>> GetTaxesForLookup()
    {
        _logger.LogInformation("Getting taxes for lookup");
        var result = await _taxService.GetTaxesForLookupAsync();
        return CreateResponse(result);
    }
}
