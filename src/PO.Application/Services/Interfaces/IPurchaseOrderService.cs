using PO.Shared.DTOs.PurchaseOrder;
using PO.Shared.Common;

namespace PO.Application.Services.Interfaces;

public interface IPurchaseOrderService
{
    Task<ApiResult<POHeaderDto>> CreatePOAsync(CreatePOHeaderDto createDto);
    Task<ApiResult<POHeaderDto>> GetPOByIdAsync(int id);
    Task<ApiResult<POHeaderDto>> UpdatePOAsync(int id, UpdatePOHeaderDto updateDto);
    Task<ApiResult<bool>> SubmitPOForApprovalAsync(int poId);
}
