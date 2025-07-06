// FILE LOCATION: src/PO.Application/Services/Interfaces/IDivisionService.cs

using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

public interface IDivisionService
{
    Task<ApiResult<IEnumerable<DivisionDto>>> GetAllDivisionsAsync();
    Task<ApiResult<PagedResult<DivisionDto>>> GetPagedDivisionsAsync(PagedRequest request);
    Task<ApiResult<DivisionDto>> GetDivisionByIdAsync(int id);
    Task<ApiResult<DivisionDto>> CreateDivisionAsync(CreateDivisionDto createDivisionDto);
    Task<ApiResult<DivisionDto>> UpdateDivisionAsync(int id, UpdateDivisionDto updateDivisionDto);
    Task<ApiResult<bool>> DeleteDivisionAsync(int id);
    Task<ApiResult<IEnumerable<DivisionLookupDto>>> GetDivisionsForLookupAsync();
}
