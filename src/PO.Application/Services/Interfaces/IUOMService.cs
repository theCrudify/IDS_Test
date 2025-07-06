// FILE LOCATION: src/PO.Application/Services/Interfaces/IUOMService.cs

using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

public interface IUOMService
{
    Task<ApiResult<IEnumerable<UOMDto>>> GetAllUOMsAsync();
    Task<ApiResult<PagedResult<UOMDto>>> GetPagedUOMsAsync(PagedRequest request);
    Task<ApiResult<UOMDto>> GetUOMByIdAsync(int id);
    Task<ApiResult<UOMDto>> CreateUOMAsync(CreateUOMDto createUOMDto);
    Task<ApiResult<UOMDto>> UpdateUOMAsync(int id, UpdateUOMDto updateUOMDto);
    Task<ApiResult<bool>> DeleteUOMAsync(int id);
    Task<ApiResult<IEnumerable<UOMLookupDto>>> GetUOMsForLookupAsync();
}
