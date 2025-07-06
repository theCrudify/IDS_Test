// FILE LOCATION: src/PO.Domain/Entities/MasterData/Item.cs
// DESCRIPTION: Item entity for managing product/service catalog with specifications

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// Item entity - represents products and services that can be purchased
/// </summary>
public class Item : BaseEntity
{
    /// <summary>
    /// Unique item code from SAP (e.g., "XV-388-032", "MIS-185")
    /// </summary>
    [Required]
    [StringLength(20)]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// Item description/name
    /// </summary>
    [Required]
    [StringLength(255)]
    public string ItemDescription { get; set; } = string.Empty;

    /// <summary>
    /// Detailed item specifications
    /// </summary>
    public string? ItemSpecification { get; set; }

    /// <summary>
    /// Item type - Barang (Goods) or Jasa (Services)
    /// </summary>
    [Required]
    public ItemType ItemType { get; set; }

    /// <summary>
    /// Item category (Paint, Chemical, Service, etc.)
    /// </summary>
    [StringLength(50)]
    public string? ItemCategory { get; set; }

    /// <summary>
    /// Brand or manufacturer
    /// </summary>
    [StringLength(100)]
    public string? Brand { get; set; }

    /// <summary>
    /// Default unit of measure ID
    /// </summary>
    public int? DefaultUOMId { get; set; }

    /// <summary>
    /// Standard unit price (for reference)
    /// </summary>
    [Column(TypeName = "decimal(18,4)")]
    public decimal? StandardPrice { get; set; }

    /// <summary>
    /// Default tax ID for this item
    /// </summary>
    public int? DefaultTaxId { get; set; }

    /// <summary>
    /// Minimum order quantity
    /// </summary>
    [Column(TypeName = "decimal(18,3)")]
    public decimal? MinOrderQty { get; set; }

    /// <summary>
    /// Lead time in days
    /// </summary>
    public int? LeadTimeDays { get; set; }

    /// <summary>
    /// Whether this item is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Additional notes about the item
    /// </summary>
    public string? Notes { get; set; }

    // Navigation properties
    /// <summary>
    /// Default unit of measure
    /// </summary>
    [ForeignKey("DefaultUOMId")]
    public virtual UOM? DefaultUOM { get; set; }

    /// <summary>
    /// Default tax for this item
    /// </summary>
    [ForeignKey("DefaultTaxId")]
    public virtual Tax? DefaultTax { get; set; }

    /// <summary>
    /// Purchase order detail lines using this item
    /// </summary>
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}