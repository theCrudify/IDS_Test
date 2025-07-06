// FILE LOCATION: src/PO.Application/Services/UOMService.cs

using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services;

public class UOMService : IUOMService
{
    private readonly IGenericRepository<UOM> _uomRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UOMService> _logger;

    public UOMService(
        IGenericRepository<UOM> uomRepository,
        IMapper mapper,
        ILogger<UOMService> logger)
    {
        _uomRepository = uomRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<UOMDto>>> GetAllUOMsAsync()
    {
        try
        {
            var uoms = await _uomRepository.GetAllAsync();
            var uomDtos = _mapper.Map<IEnumerable<UOMDto>>(uoms);
            return ApiResult<IEnumerable<UOMDto>>.SuccessResult(uomDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all UOMs");
            return ApiResult<IEnumerable<UOMDto>>.ErrorResult("Failed to retrieve UOMs", 500);
        }
    }

    public async Task<ApiResult<PagedResult<UOMDto>>> GetPagedUOMsAsync(PagedRequest request)
    {
        try
        {
            var pagedUOMs = await _uomRepository.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                u => string.IsNullOrEmpty(request.SearchTerm) || 
                     u.UOMDescription.Contains(request.SearchTerm) || 
                     u.UOMCode.Contains(request.SearchTerm),
                u => u.CreatedAt);

            var uomDtos = _mapper.Map<IEnumerable<UOMDto>>(pagedUOMs.Items);
            
            var result = new PagedResult<UOMDto>
            {
                Items = uomDtos,
                TotalCount = pagedUOMs.TotalCount,
                PageNumber = pagedUOMs.PageNumber,
                PageSize = pagedUOMs.PageSize
            };

            return ApiResult<PagedResult<UOMDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged UOMs");
            return ApiResult<PagedResult<UOMDto>>.ErrorResult("Failed to retrieve paged UOMs", 500);
        }
    }

    public async Task<ApiResult<UOMDto>> GetUOMByIdAsync(int id)
    {
        try
        {
            var uom = await _uomRepository.GetByIdAsync(id);
            if (uom == null)
                return ApiResult<UOMDto>.NotFoundResult("UOM not found");

            var uomDto = _mapper.Map<UOMDto>(uom);
            return ApiResult<UOMDto>.SuccessResult(uomDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting UOM by id {UOMId}", id);
            return ApiResult<UOMDto>.ErrorResult("Failed to retrieve UOM", 500);
        }
    }

    public async Task<ApiResult<UOMDto>> CreateUOMAsync(CreateUOMDto createUOMDto)
    {
        try
        {
            var existingUOM = await _uomRepository.FindAsync(u => u.UOMCode == createUOMDto.UOMCode);
            if (existingUOM.Any())
                return ApiResult<UOMDto>.ErrorResult("UOM code already exists", 409);

            var uom = _mapper.Map<UOM>(createUOMDto);
            uom.CreatedAt = DateTime.UtcNow;

            var createdUOM = await _uomRepository.AddAsync(uom);
            var uomDto = _mapper.Map<UOMDto>(createdUOM);

            return ApiResult<UOMDto>.SuccessResult(uomDto, "UOM created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating UOM");
            return ApiResult<UOMDto>.ErrorResult("Failed to create UOM", 500);
        }
    }

    public async Task<ApiResult<UOMDto>> UpdateUOMAsync(int id, UpdateUOMDto updateUOMDto)
    {
        try
        {
            var uom = await _uomRepository.GetByIdAsync(id);
            if (uom == null)
                return ApiResult<UOMDto>.NotFoundResult("UOM not found");

            _mapper.Map(updateUOMDto, uom);
            uom.UpdatedAt = DateTime.UtcNow;

            var updatedUOM = await _uomRepository.UpdateAsync(uom);
            var uomDto = _mapper.Map<UOMDto>(updatedUOM);

            return ApiResult<UOMDto>.SuccessResult(uomDto, "UOM updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating UOM {UOMId}", id);
            return ApiResult<UOMDto>.ErrorResult("Failed to update UOM", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteUOMAsync(int id)
    {
        try
        {
            var uom = await _uomRepository.GetByIdAsync(id);
            if (uom == null)
                return ApiResult<bool>.NotFoundResult("UOM not found");

            await _uomRepository.DeleteAsync(uom);
            return ApiResult<bool>.SuccessResult(true, "UOM deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting UOM {UOMId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete UOM", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<UOMLookupDto>>> GetUOMsForLookupAsync()
    {
        try
        {
            var uoms = await _uomRepository.FindAsync(u => u.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<UOMLookupDto>>(uoms);
            return ApiResult<IEnumerable<UOMLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting UOMs for lookup");
            return ApiResult<IEnumerable<UOMLookupDto>>.ErrorResult("Failed to retrieve UOMs for lookup", 500);
        }
    }
}
