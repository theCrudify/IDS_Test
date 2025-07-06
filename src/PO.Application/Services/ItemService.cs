// FILE LOCATION: src/PO.Application/Services/ItemService.cs

using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;
using System.Linq.Expressions;

namespace PO.Application.Services;

public class ItemService : IItemService
{
    private readonly IGenericRepository<Item> _itemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ItemService> _logger;

    public ItemService(
        IGenericRepository<Item> itemRepository,
        IMapper mapper,
        ILogger<ItemService> logger)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<ItemDto>>> GetAllItemsAsync()
    {
        try
        {
            var items = await _itemRepository.GetWithIncludesAsync(i => i.DefaultUOM, i => i.DefaultTax);
            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(items);
            return ApiResult<IEnumerable<ItemDto>>.SuccessResult(itemDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all items");
            return ApiResult<IEnumerable<ItemDto>>.ErrorResult("Failed to retrieve items", 500);
        }
    }

    public async Task<ApiResult<PagedResult<ItemDto>>> GetPagedItemsAsync(PagedRequest request)
    {
        try
        {
            Expression<Func<Item, bool>> predicate = i => 
                string.IsNullOrEmpty(request.SearchTerm) || 
                i.ItemName.Contains(request.SearchTerm) || 
                i.ItemCode.Contains(request.SearchTerm);

            var pagedItems = await _itemRepository.GetPagedWithIncludesAsync(
                request,
                i => i.DefaultUOM, i => i.DefaultTax
            );

            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(pagedItems.Items);

            var result = new PagedResult<ItemDto>
            {
                Items = itemDtos,
                TotalCount = pagedItems.TotalCount,
                PageNumber = pagedItems.PageNumber,
                PageSize = pagedItems.PageSize
            };

            return ApiResult<PagedResult<ItemDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged items");
            return ApiResult<PagedResult<ItemDto>>.ErrorResult("Failed to retrieve paged items", 500);
        }
    }

    public async Task<ApiResult<ItemDto>> GetItemByIdAsync(int id)
    {
        try
        {
            var item = (await _itemRepository.GetWithIncludesAsync(i => i.DefaultUOM, i => i.DefaultTax)).FirstOrDefault(i => i.Id == id);
            if (item == null)
                return ApiResult<ItemDto>.NotFoundResult("Item not found");

            var itemDto = _mapper.Map<ItemDto>(item);
            return ApiResult<ItemDto>.SuccessResult(itemDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting item by id {ItemId}", id);
            return ApiResult<ItemDto>.ErrorResult("Failed to retrieve item", 500);
        }
    }

    public async Task<ApiResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
    {
        try
        {
            var existingItem = await _itemRepository.FindAsync(i => i.ItemCode == createItemDto.ItemCode);
            if (existingItem.Any())
                return ApiResult<ItemDto>.ErrorResult("Item code already exists", 409);

            var item = _mapper.Map<Item>(createItemDto);
            item.CreatedAt = DateTime.UtcNow;

            var createdItem = await _itemRepository.AddAsync(item);
            var itemDto = _mapper.Map<ItemDto>(createdItem);

            return ApiResult<ItemDto>.SuccessResult(itemDto, "Item created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating item");
            return ApiResult<ItemDto>.ErrorResult("Failed to create item", 500);
        }
    }

    public async Task<ApiResult<ItemDto>> UpdateItemAsync(int id, UpdateItemDto updateItemDto)
    {
        try
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null)
                return ApiResult<ItemDto>.NotFoundResult("Item not found");

            _mapper.Map(updateItemDto, item);
            item.UpdatedAt = DateTime.UtcNow;

            var updatedItem = await _itemRepository.UpdateAsync(item);
            var itemDto = _mapper.Map<ItemDto>(updatedItem);

            return ApiResult<ItemDto>.SuccessResult(itemDto, "Item updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating item {ItemId}", id);
            return ApiResult<ItemDto>.ErrorResult("Failed to update item", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteItemAsync(int id)
    {
        try
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null)
                return ApiResult<bool>.NotFoundResult("Item not found");

            await _itemRepository.DeleteAsync(item);
            return ApiResult<bool>.SuccessResult(true, "Item deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting item {ItemId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete item", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<ItemLookupDto>>> GetItemsForLookupAsync()
    {
        try
        {
            var items = await _itemRepository.FindAsync(i => i.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<ItemLookupDto>>(items);
            return ApiResult<IEnumerable<ItemLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting items for lookup");
            return ApiResult<IEnumerable<ItemLookupDto>>.ErrorResult("Failed to retrieve items for lookup", 500);
        }
    }
}
