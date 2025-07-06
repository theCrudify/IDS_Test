using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

public class Item : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string ItemCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ItemName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    public ItemType ItemType { get; set; } = ItemType.Barang;

    [MaxLength(20)]
    public string? Brand { get; set; }

    [MaxLength(20)]
    public string? Model { get; set; }

    [MaxLength(50)]
    public string? Specification { get; set; }

    public int? DefaultUOMId { get; set; }
    public int? DefaultTaxId { get; set; }
    public decimal? StandardPrice { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual UOM? DefaultUOM { get; set; }
    public virtual Tax? DefaultTax { get; set; }
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}