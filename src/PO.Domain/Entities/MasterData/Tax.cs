using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

public class Tax : BaseEntity
{
    [Required]
    [MaxLength(10)]
    public string TaxCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string TaxDescription { get; set; } = string.Empty;

    [MaxLength(20)]
    public string TaxType { get; set; } = "VAT";

    public decimal TaxRate { get; set; } = 0.0m;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Item> DefaultItems { get; set; } = new List<Item>();
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}