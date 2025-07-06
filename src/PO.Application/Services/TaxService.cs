// FILE LOCATION: src/PO.Application/Services/TaxService.cs

using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services;

public class TaxService : ITaxService
{
    private readonly IGenericRepository<Tax> _taxRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TaxService> _logger;

    public TaxService(
        IGenericRepository<Tax> taxRepository,
        IMapper mapper,
        ILogger<TaxService> logger)
    {
        _taxRepository = taxRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<TaxDto>>> GetAllTaxesAsync()
    {
        try
        {
            var taxes = await _taxRepository.GetAllAsync();
            var taxDtos = _mapper.Map<IEnumerable<TaxDto>>(taxes);
            return ApiResult<IEnumerable<TaxDto>>.SuccessResult(taxDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all taxes");
            return ApiResult<IEnumerable<TaxDto>>.ErrorResult("Failed to retrieve taxes", 500);
        }
    }

    public async Task<ApiResult<PagedResult<TaxDto>>> GetPagedTaxesAsync(PagedRequest request)
    {
        try
        {
            var pagedTaxes = await _taxRepository.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                t => string.IsNullOrEmpty(request.SearchTerm) || 
                     t.TaxDescription.Contains(request.SearchTerm) || 
                     t.TaxCode.Contains(request.SearchTerm),
                t => t.CreatedAt);

            var taxDtos = _mapper.Map<IEnumerable<TaxDto>>(pagedTaxes.Items);
            
            var result = new PagedResult<TaxDto>
            {
                Items = taxDtos,
                TotalCount = pagedTaxes.TotalCount,
                PageNumber = pagedTaxes.PageNumber,
                PageSize = pagedTaxes.PageSize
            };

            return ApiResult<PagedResult<TaxDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged taxes");
            return ApiResult<PagedResult<TaxDto>>.ErrorResult("Failed to retrieve paged taxes", 500);
        }
    }

    public async Task<ApiResult<TaxDto>> GetTaxByIdAsync(int id)
    {
        try
        {
            var tax = await _taxRepository.GetByIdAsync(id);
            if (tax == null)
                return ApiResult<TaxDto>.NotFoundResult("Tax not found");

            var taxDto = _mapper.Map<TaxDto>(tax);
            return ApiResult<TaxDto>.SuccessResult(taxDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tax by id {TaxId}", id);
            return ApiResult<TaxDto>.ErrorResult("Failed to retrieve tax", 500);
        }
    }

    public async Task<ApiResult<TaxDto>> CreateTaxAsync(CreateTaxDto createTaxDto)
    {
        try
        {
            var existingTax = await _taxRepository.FindAsync(t => t.TaxCode == createTaxDto.TaxCode);
            if (existingTax.Any())
                return ApiResult<TaxDto>.ErrorResult("Tax code already exists", 409);

            var tax = _mapper.Map<Tax>(createTaxDto);
            tax.CreatedAt = DateTime.UtcNow;

            var createdTax = await _taxRepository.AddAsync(tax);
            var taxDto = _mapper.Map<TaxDto>(createdTax);

            return ApiResult<TaxDto>.SuccessResult(taxDto, "Tax created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating tax");
            return ApiResult<TaxDto>.ErrorResult("Failed to create tax", 500);
        }
    }

    public async Task<ApiResult<TaxDto>> UpdateTaxAsync(int id, UpdateTaxDto updateTaxDto)
    {
        try
        {
            var tax = await _taxRepository.GetByIdAsync(id);
            if (tax == null)
                return ApiResult<TaxDto>.NotFoundResult("Tax not found");

            _mapper.Map(updateTaxDto, tax);
            tax.UpdatedAt = DateTime.UtcNow;

            var updatedTax = await _taxRepository.UpdateAsync(tax);
            var taxDto = _mapper.Map<TaxDto>(updatedTax);

            return ApiResult<TaxDto>.SuccessResult(taxDto, "Tax updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating tax {TaxId}", id);
            return ApiResult<TaxDto>.ErrorResult("Failed to update tax", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteTaxAsync(int id)
    {
        try
        {
            var tax = await _taxRepository.GetByIdAsync(id);
            if (tax == null)
                return ApiResult<bool>.NotFoundResult("Tax not found");

            await _taxRepository.DeleteAsync(tax);
            return ApiResult<bool>.SuccessResult(true, "Tax deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting tax {TaxId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete tax", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<TaxLookupDto>>> GetTaxesForLookupAsync()
    {
        try
        {
            var taxes = await _taxRepository.FindAsync(t => t.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<TaxLookupDto>>(taxes);
            return ApiResult<IEnumerable<TaxLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting taxes for lookup");
            return ApiResult<IEnumerable<TaxLookupDto>>.ErrorResult("Failed to retrieve taxes for lookup", 500);
        }
    }
}
