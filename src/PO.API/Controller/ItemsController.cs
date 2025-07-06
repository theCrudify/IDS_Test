// FILE LOCATION: src/PO.API/Controllers/ItemsController.cs

using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.API.Controllers;

/// <summary>
/// Controller for managing Item master data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Master Data - Items")]
public class ItemsController : BaseController
{
    private readonly IItemService _itemService;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IItemService itemService, ILogger<ItemsController> logger)
    {
        _itemService = itemService;
        _logger = logger;
    }

    /// <summary>
    /// Get all items
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<ItemDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<ItemDto>>>> GetAllItems()
    {
        _logger.LogInformation("Getting all items");
        var result = await _itemService.GetAllItemsAsync();
        return CreateResponse(result);
    }

    /// <summary>
    /// Get items with pagination
    /// </summary>
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResult<PagedResult<ItemDto>>), 200)]
    public async Task<ActionResult<ApiResult<PagedResult<ItemDto>>>> GetPagedItems([FromQuery] PagedRequest request)
    {
        _logger.LogInformation("Getting paged items with parameters: {@Request}", request);
        var result = await _itemService.GetPagedItemsAsync(request);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get item by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<ItemDto>), 200)]
    public async Task<ActionResult<ApiResult<ItemDto>>> GetItemById(int id)
    {
        _logger.LogInformation("Getting item by ID: {ItemId}", id);
        var result = await _itemService.GetItemByIdAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Create a new item
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<ItemDto>), 201)]
    public async Task<ActionResult<ApiResult<ItemDto>>> CreateItem([FromBody] CreateItemDto createItemDto)
    {
        _logger.LogInformation("Creating new item with code: {ItemCode}", createItemDto.ItemCode);
        var result = await _itemService.CreateItemAsync(createItemDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Update an existing item
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<ItemDto>), 200)]
    public async Task<ActionResult<ApiResult<ItemDto>>> UpdateItem(int id, [FromBody] UpdateItemDto updateItemDto)
    {
        _logger.LogInformation("Updating item ID: {ItemId}", id);
        var result = await _itemService.UpdateItemAsync(id, updateItemDto);
        return CreateResponse(result);
    }

    /// <summary>
    /// Delete an item
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResult<bool>), 200)]
    public async Task<ActionResult<ApiResult<bool>>> DeleteItem(int id)
    {
        _logger.LogInformation("Deleting item ID: {ItemId}", id);
        var result = await _itemService.DeleteItemAsync(id);
        return CreateResponse(result);
    }

    /// <summary>
    /// Get items for lookup/dropdown purposes
    /// </summary>
    [HttpGet("lookup")]
    [ProducesResponseType(typeof(ApiResult<IEnumerable<ItemLookupDto>>), 200)]
    public async Task<ActionResult<ApiResult<IEnumerable<ItemLookupDto>>>> GetItemsForLookup()
    {
        _logger.LogInformation("Getting items for lookup");
        var result = await _itemService.GetItemsForLookupAsync();
        return CreateResponse(result);
    }
}
