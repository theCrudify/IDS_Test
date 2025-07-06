using System.ComponentModel.DataAnnotations;
using PO.Domain.Enums;

namespace PO.Shared.DTOs.PurchaseOrder;

public class UpdatePOHeaderDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string PONumber { get; set; } = string.Empty;

    public DateTime PODate { get; set; } = DateTime.Now;
    public DateTime PostingDate { get; set; } = DateTime.Now;
    public POType POType { get; set; } = POType.Local;

    [Required]
    [MaxLength(20)]
    public string VendorId { get; set; } = string.Empty;

    public int DeptId { get; set; }

    [MaxLength(200)]
    public string? Notes { get; set; }

    [MaxLength(200)]
    public string? DeliveryAddress { get; set; }

    public DateTime? DeliveryDate { get; set; }

    [MaxLength(3)]
    public string Currency { get; set; } = "IDR";

    public decimal ExchangeRate { get; set; } = 1.0m;

    public List<UpdatePODetailDto> PODetails { get; set; } = new List<UpdatePODetailDto>();
}
