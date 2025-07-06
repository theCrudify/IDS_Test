// FILE LOCATION: src/PO.Application/Services/Interfaces/ITaxService.cs

using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

public interface ITaxService
{
    Task<ApiResult<IEnumerable<TaxDto>>> GetAllTaxesAsync();
    Task<ApiResult<PagedResult<TaxDto>>> GetPagedTaxesAsync(PagedRequest request);
    Task<ApiResult<TaxDto>> GetTaxByIdAsync(int id);
    Task<ApiResult<TaxDto>> CreateTaxAsync(CreateTaxDto createTaxDto);
    Task<ApiResult<TaxDto>> UpdateTaxAsync(int id, UpdateTaxDto updateTaxDto);
    Task<ApiResult<bool>> DeleteTaxAsync(int id);
    Task<ApiResult<IEnumerable<TaxLookupDto>>> GetTaxesForLookupAsync();
}
