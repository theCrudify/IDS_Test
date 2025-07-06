using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services.Interfaces;

/// <summary>
/// Service interface for Role operations
/// </summary>
public interface IRoleService
{
    // Role Management
    Task<ApiResult<IEnumerable<RoleDto>>> GetAllRolesAsync();
    Task<ApiResult<PagedResult<RoleDto>>> GetPagedRolesAsync(PagedRequest request);
    Task<ApiResult<RoleDto>> GetRoleByIdAsync(int id);
    Task<ApiResult<RoleDto>> CreateRoleAsync(CreateRoleDto createRoleDto);
    Task<ApiResult<RoleDto>> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto);
    Task<ApiResult<bool>> DeleteRoleAsync(int id);
    Task<ApiResult<IEnumerable<RoleLookupDto>>> GetRolesForLookupAsync();
}