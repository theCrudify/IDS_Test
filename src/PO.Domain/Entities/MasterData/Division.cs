using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

public class Division : BaseEntity
{
    [Required]
    [MaxLength(10)]
    public string DivisionCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string DivisionName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
}