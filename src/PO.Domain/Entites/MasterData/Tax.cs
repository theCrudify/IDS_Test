// FILE LOCATION: src/PO.Domain/Entities/MasterData/Tax.cs
// DESCRIPTION: Tax entity for managing different tax types and rates

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// Tax entity - represents different tax types and their rates
/// </summary>
public class Tax : BaseEntity
{
    /// <summary>
    /// Tax code (e.g., "M11", "M12", "VAT10")
    /// </summary>
    [Required]
    [StringLength(10)]
    public string TaxCode { get; set; } = string.Empty;

    /// <summary>
    /// Tax rate as percentage (e.g., 11.00 for 11% VAT)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal TaxRate { get; set; }

    /// <summary>
    /// Description of the tax
    /// </summary>
    [StringLength(100)]
    public string? TaxDescription { get; set; }

    /// <summary>
    /// Tax type (VAT, Income Tax, etc.)
    /// </summary>
    [StringLength(50)]
    public string? TaxType { get; set; }

    /// <summary>
    /// Whether this tax is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Effective date from when this tax rate is applicable
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// Expiry date when this tax rate stops being applicable
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    // Navigation properties
    /// <summary>
    /// Items that use this tax as default
    /// </summary>
    public virtual ICollection<Item> DefaultItems { get; set; } = new List<Item>();

    /// <summary>
    /// Purchase order details using this tax
    /// </summary>
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}