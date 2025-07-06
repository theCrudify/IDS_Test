using PO.Shared.Common;
using PO.Shared.DTOs.PurchaseOrder;

namespace PO.Application.Services.Interfaces;

public interface IApprovalService
{
    Task<ApiResult<bool>> ApprovePOAsync(int poId, int userId, string? notes);
    Task<ApiResult<bool>> RejectPOAsync(int poId, int userId, string reason);
    Task<ApiResult<List<POHeaderDto>>> GetPendingApprovalsForUserAsync(int userId);
}
