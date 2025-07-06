using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services;

public class RoleService : IRoleService
{
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RoleService> _logger;

    public RoleService(
        IGenericRepository<Role> roleRepository,
        IMapper mapper,
        ILogger<RoleService> logger)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<RoleDto>>> GetAllRolesAsync()
    {
        try
        {
            var roles = await _roleRepository.GetAllAsync();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return ApiResult<IEnumerable<RoleDto>>.SuccessResult(roleDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all roles");
            return ApiResult<IEnumerable<RoleDto>>.ErrorResult("Failed to retrieve roles", 500);
        }
    }

    public async Task<ApiResult<PagedResult<RoleDto>>> GetPagedRolesAsync(PagedRequest request)
    {
        try
        {
            var pagedRoles = await _roleRepository.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                r => string.IsNullOrEmpty(request.SearchTerm) || 
                     r.RoleName.Contains(request.SearchTerm) || 
                     r.RoleCode.Contains(request.SearchTerm),
                r => r.CreatedAt);

            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(pagedRoles.Items);
            
            var result = new PagedResult<RoleDto>
            {
                Items = roleDtos,
                TotalCount = pagedRoles.TotalCount,
                PageNumber = pagedRoles.PageNumber,
                PageSize = pagedRoles.PageSize
            };

            return ApiResult<PagedResult<RoleDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged roles");
            return ApiResult<PagedResult<RoleDto>>.ErrorResult("Failed to retrieve paged roles", 500);
        }
    }

    public async Task<ApiResult<RoleDto>> GetRoleByIdAsync(int id)
    {
        try
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                return ApiResult<RoleDto>.NotFoundResult("Role not found");

            var roleDto = _mapper.Map<RoleDto>(role);
            return ApiResult<RoleDto>.SuccessResult(roleDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting role by id {RoleId}", id);
            return ApiResult<RoleDto>.ErrorResult("Failed to retrieve role", 500);
        }
    }

    public async Task<ApiResult<RoleDto>> CreateRoleAsync(CreateRoleDto createRoleDto)
    {
        try
        {
            // Check if role code already exists
            var existingRole = await _roleRepository.FindAsync(r => r.RoleCode == createRoleDto.RoleCode);
            if (existingRole.Any())
                return ApiResult<RoleDto>.ErrorResult("Role code already exists", 409);

            var role = _mapper.Map<Role>(createRoleDto);
            role.CreatedAt = DateTime.UtcNow;

            var createdRole = await _roleRepository.AddAsync(role);
            var roleDto = _mapper.Map<RoleDto>(createdRole);

            return ApiResult<RoleDto>.SuccessResult(roleDto, "Role created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating role");
            return ApiResult<RoleDto>.ErrorResult("Failed to create role", 500);
        }
    }

    public async Task<ApiResult<RoleDto>> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto)
    {
        try
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                return ApiResult<RoleDto>.NotFoundResult("Role not found");

            _mapper.Map(updateRoleDto, role);
            role.UpdatedAt = DateTime.UtcNow;

            var updatedRole = await _roleRepository.UpdateAsync(role);
            var roleDto = _mapper.Map<RoleDto>(updatedRole);

            return ApiResult<RoleDto>.SuccessResult(roleDto, "Role updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating role {RoleId}", id);
            return ApiResult<RoleDto>.ErrorResult("Failed to update role", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteRoleAsync(int id)
    {
        try
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                return ApiResult<bool>.NotFoundResult("Role not found");

            await _roleRepository.DeleteAsync(role);
            return ApiResult<bool>.SuccessResult(true, "Role deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting role {RoleId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete role", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<RoleLookupDto>>> GetRolesForLookupAsync()
    {
        try
        {
            var roles = await _roleRepository.FindAsync(r => r.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<RoleLookupDto>>(roles);
            return ApiResult<IEnumerable<RoleLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting roles for lookup");
            return ApiResult<IEnumerable<RoleLookupDto>>.ErrorResult("Failed to retrieve roles for lookup", 500);
        }
    }
}