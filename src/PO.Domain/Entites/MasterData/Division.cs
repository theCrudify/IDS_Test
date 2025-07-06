// FILE LOCATION: src/PO.Domain/Entities/MasterData/Division.cs
// DESCRIPTION: Division entity for organizing items and costs by business divisions

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// Division entity - represents business divisions for cost allocation
/// </summary>
public class Division : BaseEntity
{
    /// <summary>
    /// Short division code (e.g., "TECH", "PROD", "ADMIN")
    /// </summary>
    [Required]
    [StringLength(20)]
    public string DivisionCode { get; set; } = string.Empty;

    /// <summary>
    /// Full division name (e.g., "Technical Division", "Production Division")
    /// </summary>
    [Required]
    [StringLength(50)]
    public string DivisionName { get; set; } = string.Empty;

    /// <summary>
    /// Description of the division's purpose
    /// </summary>
    [StringLength(200)]
    public string? Description { get; set; }

    /// <summary>
    /// Budget allocated to this division
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Budget { get; set; }

    /// <summary>
    /// Cost center code for accounting
    /// </summary>
    [StringLength(20)]
    public string? CostCenter { get; set; }

    /// <summary>
    /// Whether this division is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    /// <summary>
    /// Purchase order details allocated to this division
    /// </summary>
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}