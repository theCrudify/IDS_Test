// FILE LOCATION: src/PO.Domain/Entities/PurchaseOrder/PODetail.cs
// DESCRIPTION: Purchase Order Detail entity - individual line items in a PO

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Entities.MasterData;

namespace PO.Domain.Entities.PurchaseOrder;

/// <summary>
/// Purchase Order Detail entity - represents individual line items in a purchase order
/// </summary>
public class PODetail : BaseEntity
{
    /// <summary>
    /// Purchase Order Header ID (foreign key)
    /// </summary>
    [Required]
    public int POId { get; set; }

    /// <summary>
    /// Line number within the PO (1, 2, 3, etc.)
    /// </summary>
    [Required]
    public int LineNumber { get; set; }

    /// <summary>
    /// Item code (foreign key to Item entity)
    /// </summary>
    [Required]
    [StringLength(20)]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// Item description (can override master data)
    /// </summary>
    [Required]
    [StringLength(255)]
    public string ItemDescription { get; set; } = string.Empty;

    /// <summary>
    /// Detailed item specification for this PO line
    /// </summary>
    public string? ItemSpecification { get; set; }

    /// <summary>
    /// Quantity ordered
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,3)")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Unit of measure ID (foreign key to UOM entity)
    /// </summary>
    [Required]
    public int UOMId { get; set; }

    /// <summary>
    /// Unit price for this item
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Total price for this line (Quantity × UnitPrice - RowDiscountAmount)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Tax ID for this line (foreign key to Tax entity)
    /// </summary>
    [Required]
    public int TaxId { get; set; }

    /// <summary>
    /// Tax amount for this line
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    /// <summary>
    /// Required delivery date for this line
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// Division ID for cost allocation (foreign key to Division entity)
    /// </summary>
    public int? DivisionId { get; set; }

    // Additional SAP fields
    /// <summary>
    /// Number per measure (conversion factor)
    /// </summary>
    [Column(TypeName = "decimal(10,3)")]
    public decimal NumPerMsr { get; set; } = 1.000m;

    /// <summary>
    /// Second number per measure
    /// </summary>
    [Column(TypeName = "decimal(10,3)")]
    public decimal NumPerMsr2 { get; set; } = 1.000m;

    /// <summary>
    /// Second UOM code (if different from primary)
    /// </summary>
    [StringLength(10)]
    public string? UOMCode2 { get; set; }

    /// <summary>
    /// Row currency (can be different from header currency)
    /// </summary>
    [StringLength(5)]
    public string RowCurrency { get; set; } = "IDR";

    /// <summary>
    /// Discount percentage for this line
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal RowDiscountPercent { get; set; } = 0.00m;

    /// <summary>
    /// Discount amount for this line
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal RowDiscountAmount { get; set; } = 0.00m;

    /// <summary>
    /// Additional notes for this line item
    /// </summary>
    public string? LineNotes { get; set; }

    // Navigation properties
    /// <summary>
    /// Purchase Order Header
    /// </summary>
    [ForeignKey("POId")]
    public virtual POHeader POHeader { get; set; } = null!;

    /// <summary>
    /// Item master data
    /// </summary>
    [ForeignKey("ItemCode")]
    public virtual Item Item { get; set; } = null!;

    /// <summary>
    /// Unit of measure
    /// </summary>
    [ForeignKey("UOMId")]
    public virtual UOM UOM { get; set; } = null!;

    /// <summary>
    /// Tax information
    /// </summary>
    [ForeignKey("TaxId")]
    public virtual Tax Tax { get; set; } = null!;

    /// <summary>
    /// Division for cost allocation
    /// </summary>
    [ForeignKey("DivisionId")]
    public virtual Division? Division { get; set; }
}