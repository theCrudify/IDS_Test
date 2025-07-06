// FILE LOCATION: src/PO.Application/Services/DepartmentService.cs

using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IGenericRepository<Department> _departmentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(
        IGenericRepository<Department> departmentRepository,
        IMapper mapper,
        ILogger<DepartmentService> logger)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync()
    {
        try
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return ApiResult<IEnumerable<DepartmentDto>>.SuccessResult(departmentDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all departments");
            return ApiResult<IEnumerable<DepartmentDto>>.ErrorResult("Failed to retrieve departments", 500);
        }
    }

    public async Task<ApiResult<PagedResult<DepartmentDto>>> GetPagedDepartmentsAsync(PagedRequest request)
    {
        try
        {
            var pagedDepartments = await _departmentRepository.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                d => string.IsNullOrEmpty(request.SearchTerm) || 
                     d.DeptName.Contains(request.SearchTerm) || 
                     d.DeptCode.Contains(request.SearchTerm),
                d => d.CreatedAt);

            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(pagedDepartments.Items);
            
            var result = new PagedResult<DepartmentDto>
            {
                Items = departmentDtos,
                TotalCount = pagedDepartments.TotalCount,
                PageNumber = pagedDepartments.PageNumber,
                PageSize = pagedDepartments.PageSize
            };

            return ApiResult<PagedResult<DepartmentDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged departments");
            return ApiResult<PagedResult<DepartmentDto>>.ErrorResult("Failed to retrieve paged departments", 500);
        }
    }

    public async Task<ApiResult<DepartmentDto>> GetDepartmentByIdAsync(int id)
    {
        try
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return ApiResult<DepartmentDto>.NotFoundResult("Department not found");

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return ApiResult<DepartmentDto>.SuccessResult(departmentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting department by id {DepartmentId}", id);
            return ApiResult<DepartmentDto>.ErrorResult("Failed to retrieve department", 500);
        }
    }

    public async Task<ApiResult<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
    {
        try
        {
            var existingDepartment = await _departmentRepository.FindAsync(d => d.DeptCode == createDepartmentDto.DeptCode);
            if (existingDepartment.Any())
                return ApiResult<DepartmentDto>.ErrorResult("Department code already exists", 409);

            var department = _mapper.Map<Department>(createDepartmentDto);
            department.CreatedAt = DateTime.UtcNow;

            var createdDepartment = await _departmentRepository.AddAsync(department);
            var departmentDto = _mapper.Map<DepartmentDto>(createdDepartment);

            return ApiResult<DepartmentDto>.SuccessResult(departmentDto, "Department created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating department");
            return ApiResult<DepartmentDto>.ErrorResult("Failed to create department", 500);
        }
    }

    public async Task<ApiResult<DepartmentDto>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto)
    {
        try
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return ApiResult<DepartmentDto>.NotFoundResult("Department not found");

            _mapper.Map(updateDepartmentDto, department);
            department.UpdatedAt = DateTime.UtcNow;

            var updatedDepartment = await _departmentRepository.UpdateAsync(department);
            var departmentDto = _mapper.Map<DepartmentDto>(updatedDepartment);

            return ApiResult<DepartmentDto>.SuccessResult(departmentDto, "Department updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating department {DepartmentId}", id);
            return ApiResult<DepartmentDto>.ErrorResult("Failed to update department", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteDepartmentAsync(int id)
    {
        try
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return ApiResult<bool>.NotFoundResult("Department not found");

            await _departmentRepository.DeleteAsync(department);
            return ApiResult<bool>.SuccessResult(true, "Department deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting department {DepartmentId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete department", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<DepartmentLookupDto>>> GetDepartmentsForLookupAsync()
    {
        try
        {
            var departments = await _departmentRepository.FindAsync(d => d.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<DepartmentLookupDto>>(departments);
            return ApiResult<IEnumerable<DepartmentLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting departments for lookup");
            return ApiResult<IEnumerable<DepartmentLookupDto>>.ErrorResult("Failed to retrieve departments for lookup", 500);
        }
    }
}
