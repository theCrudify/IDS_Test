using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

public class UOM : BaseEntity
{
    [Required]
    [MaxLength(10)]
    public string UOMCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string UOMDescription { get; set; } = string.Empty;

    [MaxLength(10)]
    public string BaseUnit { get; set; } = string.Empty;

    public decimal ConversionFactor { get; set; } = 1.0m;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Item> DefaultItems { get; set; } = new List<Item>();
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}