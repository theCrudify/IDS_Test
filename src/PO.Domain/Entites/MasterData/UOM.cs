// FILE LOCATION: src/PO.Domain/Entities/MasterData/UOM.cs
// DESCRIPTION: Unit of Measure entity for managing different measurement units

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// UOM (Unit of Measure) entity - represents different units like PCS, KG, Liter, etc.
/// </summary>
public class UOM : BaseEntity
{
    /// <summary>
    /// Short UOM code (e.g., "PCS", "CAN3.6", "KG")
    /// </summary>
    [Required]
    [StringLength(10)]
    public string UOMCode { get; set; } = string.Empty;

    /// <summary>
    /// Full description of the unit of measure
    /// </summary>
    [StringLength(50)]
    public string? UOMDescription { get; set; }

    /// <summary>
    /// Base unit for conversion (e.g., "PCS" for pieces, "KG" for weight)
    /// </summary>
    [StringLength(10)]
    public string? BaseUnit { get; set; }

    /// <summary>
    /// Conversion factor to base unit
    /// </summary>
    [Column(TypeName = "decimal(10,6)")]
    public decimal ConversionFactor { get; set; } = 1.000000m;

    /// <summary>
    /// Whether this UOM is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    /// <summary>
    /// Items that use this UOM as default
    /// </summary>
    public virtual ICollection<Item> DefaultItems { get; set; } = new List<Item>();

    /// <summary>
    /// Purchase order details using this UOM
    /// </summary>
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}