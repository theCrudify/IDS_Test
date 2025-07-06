// FILE LOCATION: src/PO.Application/Services/Interfaces/IDepartmentService.cs

using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<ApiResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync();
    Task<ApiResult<PagedResult<DepartmentDto>>> GetPagedDepartmentsAsync(PagedRequest request);
    Task<ApiResult<DepartmentDto>> GetDepartmentByIdAsync(int id);
    Task<ApiResult<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);
    Task<ApiResult<DepartmentDto>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto);
    Task<ApiResult<bool>> DeleteDepartmentAsync(int id);
    Task<ApiResult<IEnumerable<DepartmentLookupDto>>> GetDepartmentsForLookupAsync();
}
