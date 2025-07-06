// FILE LOCATION: src/PO.Application/Services/DivisionService.cs

using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services;

public class DivisionService : IDivisionService
{
    private readonly IGenericRepository<Division> _divisionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DivisionService> _logger;

    public DivisionService(
        IGenericRepository<Division> divisionRepository,
        IMapper mapper,
        ILogger<DivisionService> logger)
    {
        _divisionRepository = divisionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<DivisionDto>>> GetAllDivisionsAsync()
    {
        try
        {
            var divisions = await _divisionRepository.GetAllAsync();
            var divisionDtos = _mapper.Map<IEnumerable<DivisionDto>>(divisions);
            return ApiResult<IEnumerable<DivisionDto>>.SuccessResult(divisionDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all divisions");
            return ApiResult<IEnumerable<DivisionDto>>.ErrorResult("Failed to retrieve divisions", 500);
        }
    }

    public async Task<ApiResult<PagedResult<DivisionDto>>> GetPagedDivisionsAsync(PagedRequest request)
    {
        try
        {
            var pagedDivisions = await _divisionRepository.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                d => string.IsNullOrEmpty(request.SearchTerm) || 
                     d.DivisionName.Contains(request.SearchTerm) || 
                     d.DivisionCode.Contains(request.SearchTerm),
                d => d.CreatedAt);

            var divisionDtos = _mapper.Map<IEnumerable<DivisionDto>>(pagedDivisions.Items);
            
            var result = new PagedResult<DivisionDto>
            {
                Items = divisionDtos,
                TotalCount = pagedDivisions.TotalCount,
                PageNumber = pagedDivisions.PageNumber,
                PageSize = pagedDivisions.PageSize
            };

            return ApiResult<PagedResult<DivisionDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged divisions");
            return ApiResult<PagedResult<DivisionDto>>.ErrorResult("Failed to retrieve paged divisions", 500);
        }
    }

    public async Task<ApiResult<DivisionDto>> GetDivisionByIdAsync(int id)
    {
        try
        {
            var division = await _divisionRepository.GetByIdAsync(id);
            if (division == null)
                return ApiResult<DivisionDto>.NotFoundResult("Division not found");

            var divisionDto = _mapper.Map<DivisionDto>(division);
            return ApiResult<DivisionDto>.SuccessResult(divisionDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting division by id {DivisionId}", id);
            return ApiResult<DivisionDto>.ErrorResult("Failed to retrieve division", 500);
        }
    }

    public async Task<ApiResult<DivisionDto>> CreateDivisionAsync(CreateDivisionDto createDivisionDto)
    {
        try
        {
            var existingDivision = await _divisionRepository.FindAsync(d => d.DivisionCode == createDivisionDto.DivisionCode);
            if (existingDivision.Any())
                return ApiResult<DivisionDto>.ErrorResult("Division code already exists", 409);

            var division = _mapper.Map<Division>(createDivisionDto);
            division.CreatedAt = DateTime.UtcNow;

            var createdDivision = await _divisionRepository.AddAsync(division);
            var divisionDto = _mapper.Map<DivisionDto>(createdDivision);

            return ApiResult<DivisionDto>.SuccessResult(divisionDto, "Division created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating division");
            return ApiResult<DivisionDto>.ErrorResult("Failed to create division", 500);
        }
    }

    public async Task<ApiResult<DivisionDto>> UpdateDivisionAsync(int id, UpdateDivisionDto updateDivisionDto)
    {
        try
        {
            var division = await _divisionRepository.GetByIdAsync(id);
            if (division == null)
                return ApiResult<DivisionDto>.NotFoundResult("Division not found");

            _mapper.Map(updateDivisionDto, division);
            division.UpdatedAt = DateTime.UtcNow;

            var updatedDivision = await _divisionRepository.UpdateAsync(division);
            var divisionDto = _mapper.Map<DivisionDto>(updatedDivision);

            return ApiResult<DivisionDto>.SuccessResult(divisionDto, "Division updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating division {DivisionId}", id);
            return ApiResult<DivisionDto>.ErrorResult("Failed to update division", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteDivisionAsync(int id)
    {
        try
        {
            var division = await _divisionRepository.GetByIdAsync(id);
            if (division == null)
                return ApiResult<bool>.NotFoundResult("Division not found");

            await _divisionRepository.DeleteAsync(division);
            return ApiResult<bool>.SuccessResult(true, "Division deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting division {DivisionId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete division", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<DivisionLookupDto>>> GetDivisionsForLookupAsync()
    {
        try
        {
            var divisions = await _divisionRepository.FindAsync(d => d.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<DivisionLookupDto>>(divisions);
            return ApiResult<IEnumerable<DivisionLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting divisions for lookup");
            return ApiResult<IEnumerable<DivisionLookupDto>>.ErrorResult("Failed to retrieve divisions for lookup", 500);
        }
    }
}
