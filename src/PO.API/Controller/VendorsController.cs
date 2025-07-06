// FILE LOCATION: src/PO.API/Controllers/VendorsController.cs

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing Vendor master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - Vendors")]
public class VendorsController : BaseController
{
    private readonly IVendorService _vendorService;
    private readonly ILogger<VendorsController> _logger;

    public VendorsController(IVendorService vendorService, ILogger<VendorsController> logger)
    {
        _vendorService = vendorService;
        _logger = logger;
    }

    /// <summary>
    /// Get all vendors
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<VendorDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<VendorDto>>>> GetAllVendors()
    {
        _logger.LogInformation("Getting all vendors");
        var result = await _vendorService.GetAllVendorsAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get vendors with pagination
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<VendorDto>>), 200)]
    public async Task<ActionResult<ApiResult<PagedResult<VendorDto>>>> GetPagedVendors([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged vendors with parameters: {@Request}", request);
        var result = await _vendorService.GetPagedVendorsAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get vendor by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<VendorDto>), 200)]
    public async Task<ActionResult<ApiResult<VendorDto>>> GetVendorById(int id)
    {
        _logger.LogInformation("Getting vendor by ID: {VendorId}", id);
        var result = await _vendorService.GetVendorByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new vendor
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<VendorDto>), 201)]
    public async Task<ActionResult<ApiResult<VendorDto>>> CreateVendor([FromBody] CreateVendorDto createVendorDto)
    {
        _logger.LogInformation("Creating new vendor with ID: {VendorId}", createVendorDto.VendorId);
        var result = await _vendorService.CreateVendorAsync(createVendorDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing vendor
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<VendorDto>), 200)]
    public async Task<ActionResult<ApiResult<VendorDto>>> UpdateVendor(int id, [FromBody] UpdateVendorDto updateVendorDto)
    {
        _logger.LogInformation("Updating vendor ID: {VendorId}", id);
        var result = await _vendorService.UpdateVendorAsync(id, updateVendorDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete a vendor
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteVendor(int id)
    {
        _logger.LogInformation("Deleting vendor ID: {VendorId}", id);
        var result = await _vendorService.DeleteVendorAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get vendors for lookup/dropdown purposes
    /// </summary>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<VendorLookupDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<VendorLookupDto>>>> GetVendorsForLookup()
    {
        _logger.LogInformation("Getting vendors for lookup");
        var result = await _vendorService.GetVendorsForLookupAsync();
        return CreateResponse(result);
    }
}
