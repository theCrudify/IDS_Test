using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

public class Department : BaseEntity
{
    [Required]
    [MaxLength(10)]
    public string DeptCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string DeptName { get; set; } = string.Empty;

    public int? DeptHeadId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual User? DeptHead { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<ApprovalMatrix> ApprovalMatrices { get; set; } = new List<ApprovalMatrix>();
    public virtual ICollection<POHeader> PurchaseOrders { get; set; } = new List<POHeader>();
}