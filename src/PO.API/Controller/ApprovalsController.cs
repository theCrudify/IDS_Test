using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.DTOs.PurchaseOrder;

namespace PO.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApprovalsController : BaseController
{
    private readonly IApprovalService _approvalService;

    public ApprovalsController(IApprovalService approvalService)
    {
        _approvalService = approvalService;
    }

    [HttpPost("{poId}/approve")]
    public async Task<IActionResult> ApprovePO(int poId, [FromQuery] int userId, [FromBody] ApprovalRequestDto requestDto)
    {
        var result = await _approvalService.ApprovePOAsync(poId, userId, requestDto.Notes);
        return HandleResult(result);
    }

    [HttpPost("{poId}/reject")]
    public async Task<IActionResult> RejectPO(int poId, [FromQuery] int userId, [FromBody] ApprovalRequestDto requestDto)
    {
        var result = await _approvalService.RejectPOAsync(poId, userId, requestDto.Reason);
        return HandleResult(result);
    }

    [HttpGet("pending/{userId}")]
    public async Task<IActionResult> GetPendingApprovalsForUser(int userId)
    {
        var result = await _approvalService.GetPendingApprovalsForUserAsync(userId);
        return HandleResult(result);
    }
}

public class ApprovalRequestDto
{
    public string? Notes { get; set; }
    public string? Reason { get; set; }
}
