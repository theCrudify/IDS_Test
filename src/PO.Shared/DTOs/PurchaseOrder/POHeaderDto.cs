using System.ComponentModel.DataAnnotations;
using PO.Domain.Enums;

namespace PO.Shared.DTOs.PurchaseOrder;

public class POHeaderDto
{
    public int Id { get; set; }
    public string PONumber { get; set; } = string.Empty;
    public DateTime PODate { get; set; }
    public DateTime PostingDate { get; set; }
    public POStatus Status { get; set; }
    public POType POType { get; set; }
    public string VendorId { get; set; } = string.Empty;
    public int DeptId { get; set; }
    public int CreatedById { get; set; }
    public string? Notes { get; set; }
    public string? DeliveryAddress { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string Currency { get; set; } = "IDR";
    public decimal ExchangeRate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalDue { get; set; }
    public string? ApprovalNotes { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string? RejectionReason { get; set; }

    public List<PODetailDto> PODetails { get; set; } = new List<PODetailDto>();
}
