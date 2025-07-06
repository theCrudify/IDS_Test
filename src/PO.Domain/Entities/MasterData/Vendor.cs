using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

public class Vendor : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string VendorId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string VendorName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? ContactPerson { get; set; }

    [MaxLength(30)]
    public string? TaxId { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<POHeader> PurchaseOrders { get; set; } = new List<POHeader>();
}