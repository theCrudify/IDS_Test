// FILE LOCATION: src/PO.Application/Services/VendorService.cs

using AutoMapper;
using Microsoft.Extensions.Logging;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.MasterData;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Services;

public class VendorService : IVendorService
{
    private readonly IGenericRepository<Vendor> _vendorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<VendorService> _logger;

    public VendorService(
        IGenericRepository<Vendor> vendorRepository,
        IMapper mapper,
        ILogger<VendorService> logger)
    {
        _vendorRepository = vendorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<IEnumerable<VendorDto>>> GetAllVendorsAsync()
    {
        try
        {
            var vendors = await _vendorRepository.GetAllAsync();
            var vendorDtos = _mapper.Map<IEnumerable<VendorDto>>(vendors);
            return ApiResult<IEnumerable<VendorDto>>.SuccessResult(vendorDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all vendors");
            return ApiResult<IEnumerable<VendorDto>>.ErrorResult("Failed to retrieve vendors", 500);
        }
    }

    public async Task<ApiResult<PagedResult<VendorDto>>> GetPagedVendorsAsync(PagedRequest request)
    {
        try
        {
            var pagedVendors = await _vendorRepository.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                v => string.IsNullOrEmpty(request.SearchTerm) || 
                     v.VendorName.Contains(request.SearchTerm) || 
                     v.VendorId.Contains(request.SearchTerm),
                v => v.CreatedAt);

            var vendorDtos = _mapper.Map<IEnumerable<VendorDto>>(pagedVendors.Items);
            
            var result = new PagedResult<VendorDto>
            {
                Items = vendorDtos,
                TotalCount = pagedVendors.TotalCount,
                PageNumber = pagedVendors.PageNumber,
                PageSize = pagedVendors.PageSize
            };

            return ApiResult<PagedResult<VendorDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged vendors");
            return ApiResult<PagedResult<VendorDto>>.ErrorResult("Failed to retrieve paged vendors", 500);
        }
    }

    public async Task<ApiResult<VendorDto>> GetVendorByIdAsync(int id)
    {
        try
        {
            var vendor = await _vendorRepository.GetByIdAsync(id);
            if (vendor == null)
                return ApiResult<VendorDto>.NotFoundResult("Vendor not found");

            var vendorDto = _mapper.Map<VendorDto>(vendor);
            return ApiResult<VendorDto>.SuccessResult(vendorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vendor by id {VendorId}", id);
            return ApiResult<VendorDto>.ErrorResult("Failed to retrieve vendor", 500);
        }
    }

    public async Task<ApiResult<VendorDto>> CreateVendorAsync(CreateVendorDto createVendorDto)
    {
        try
        {
            var existingVendor = await _vendorRepository.FindAsync(v => v.VendorId == createVendorDto.VendorId);
            if (existingVendor.Any())
                return ApiResult<VendorDto>.ErrorResult("Vendor ID already exists", 409);

            var vendor = _mapper.Map<Vendor>(createVendorDto);
            vendor.CreatedAt = DateTime.UtcNow;

            var createdVendor = await _vendorRepository.AddAsync(vendor);
            var vendorDto = _mapper.Map<VendorDto>(createdVendor);

            return ApiResult<VendorDto>.SuccessResult(vendorDto, "Vendor created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating vendor");
            return ApiResult<VendorDto>.ErrorResult("Failed to create vendor", 500);
        }
    }

    public async Task<ApiResult<VendorDto>> UpdateVendorAsync(int id, UpdateVendorDto updateVendorDto)
    {
        try
        {
            var vendor = await _vendorRepository.GetByIdAsync(id);
            if (vendor == null)
                return ApiResult<VendorDto>.NotFoundResult("Vendor not found");

            _mapper.Map(updateVendorDto, vendor);
            vendor.UpdatedAt = DateTime.UtcNow;

            var updatedVendor = await _vendorRepository.UpdateAsync(vendor);
            var vendorDto = _mapper.Map<VendorDto>(updatedVendor);

            return ApiResult<VendorDto>.SuccessResult(vendorDto, "Vendor updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vendor {VendorId}", id);
            return ApiResult<VendorDto>.ErrorResult("Failed to update vendor", 500);
        }
    }

    public async Task<ApiResult<bool>> DeleteVendorAsync(int id)
    {
        try
        {
            var vendor = await _vendorRepository.GetByIdAsync(id);
            if (vendor == null)
                return ApiResult<bool>.NotFoundResult("Vendor not found");

            await _vendorRepository.DeleteAsync(vendor);
            return ApiResult<bool>.SuccessResult(true, "Vendor deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting vendor {VendorId}", id);
            return ApiResult<bool>.ErrorResult("Failed to delete vendor", 500);
        }
    }

    public async Task<ApiResult<IEnumerable<VendorLookupDto>>> GetVendorsForLookupAsync()
    {
        try
        {
            var vendors = await _vendorRepository.FindAsync(v => v.IsActive);
            var lookupDtos = _mapper.Map<IEnumerable<VendorLookupDto>>(vendors);
            return ApiResult<IEnumerable<VendorLookupDto>>.SuccessResult(lookupDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vendors for lookup");
            return ApiResult<IEnumerable<VendorLookupDto>>.ErrorResult("Failed to retrieve vendors for lookup", 500);
        }
    }
}
