using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.PurchaseOrder;

public class CreatePODetailDto
{
    public int LineNumber { get; set; }

    [Required]
    [MaxLength(20)]
    public string ItemCode { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? ItemDescription { get; set; }

    public decimal Quantity { get; set; } = 0;
    public int UOMId { get; set; }
    public decimal UnitPrice { get; set; } = 0;
    public int? TaxId { get; set; }

    [MaxLength(200)]
    public string? Notes { get; set; }

    public DateTime? DeliveryDate { get; set; }
    public int? DivisionId { get; set; }
}
