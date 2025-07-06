using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.MasterData;

namespace PO.Domain.Entities.PurchaseOrder;

public class PODetail : BaseEntity
{
    public int POId { get; set; }
    public int LineNumber { get; set; }

    public int ItemId { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string ItemCode { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? ItemDescription { get; set; }

    public decimal Quantity { get; set; } = 0;
    public int UOMId { get; set; }
    public decimal UnitPrice { get; set; } = 0;
    public decimal LineTotal { get; set; } = 0;
    public int? TaxId { get; set; }
    public decimal TaxAmount { get; set; } = 0;
    public decimal LineTotalIncludingTax { get; set; } = 0;

    [MaxLength(200)]
    public string? Notes { get; set; }

    public DateTime? DeliveryDate { get; set; }
    public int? DivisionId { get; set; }

    // Navigation properties
    public virtual POHeader POHeader { get; set; } = null!;
    public virtual Item Item { get; set; } = null!;
    public virtual UOM UOM { get; set; } = null!;
    public virtual Tax? Tax { get; set; }
    public virtual Division? Division { get; set; }
}