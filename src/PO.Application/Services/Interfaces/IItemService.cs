// FILE LOCATION: src/PO.Application/Services/Interfaces/IItemService.cs

using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

public interface IItemService
{
    Task<ApiResult<IEnumerable<ItemDto>>> GetAllItemsAsync();
    Task<ApiResult<PagedResult<ItemDto>>> GetPagedItemsAsync(PagedRequest request);
    Task<ApiResult<ItemDto>> GetItemByIdAsync(int id);
    Task<ApiResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto);
    Task<ApiResult<ItemDto>> UpdateItemAsync(int id, UpdateItemDto updateItemDto);
    Task<ApiResult<bool>> DeleteItemAsync(int id);
    Task<ApiResult<IEnumerable<ItemLookupDto>>> GetItemsForLookupAsync();
}
