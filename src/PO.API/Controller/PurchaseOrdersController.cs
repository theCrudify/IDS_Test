using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;
using PO.Shared.DTOs.PurchaseOrder;

namespace PO.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseOrdersController : BaseController
{
    private readonly IPurchaseOrderService _purchaseOrderService;

    public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePO([FromBody] CreatePOHeaderDto createDto)
    {
        var result = await _purchaseOrderService.CreatePOAsync(createDto);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPOById(int id)
    {
        var result = await _purchaseOrderService.GetPOByIdAsync(id);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePO(int id, [FromBody] UpdatePOHeaderDto updateDto)
    {
        var result = await _purchaseOrderService.UpdatePOAsync(id, updateDto);
        return HandleResult(result);
    }

    [HttpPost("{id}/submit")]
    public async Task<IActionResult> SubmitPOForApproval(int id)
    {
        var result = await _purchaseOrderService.SubmitPOForApprovalAsync(id);
        return HandleResult(result);
    }
}
