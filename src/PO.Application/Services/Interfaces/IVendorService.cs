// FILE LOCATION: src/PO.Application/Services/Interfaces/IVendorService.cs

using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

public interface IVendorService
{
    Task<ApiResult<IEnumerable<VendorDto>>> GetAllVendorsAsync();
    Task<ApiResult<PagedResult<VendorDto>>> GetPagedVendorsAsync(PagedRequest request);
    Task<ApiResult<VendorDto>> GetVendorByIdAsync(int id);
    Task<ApiResult<VendorDto>> CreateVendorAsync(CreateVendorDto createVendorDto);
    Task<ApiResult<VendorDto>> UpdateVendorAsync(int id, UpdateVendorDto updateVendorDto);
    Task<ApiResult<bool>> DeleteVendorAsync(int id);
    Task<ApiResult<IEnumerable<VendorLookupDto>>> GetVendorsForLookupAsync();
}
