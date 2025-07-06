using System.ComponentModel.DataAnnotations;

namespace PO.Shared.DTOs.PurchaseOrder;

public class PODetailDto
{
    public int Id { get; set; }
    public int POId { get; set; }
    public int LineNumber { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string? ItemDescription { get; set; }
    public decimal Quantity { get; set; }
    public int UOMId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
    public int? TaxId { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal LineTotalIncludingTax { get; set; }
    public string? Notes { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int? DivisionId { get; set; }
}
