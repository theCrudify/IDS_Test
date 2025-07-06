using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;
using PO.Domain.Entities.System;

namespace PO.Domain.Entities.MasterData;

public class User : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    public int RoleId { get; set; }
    public int DeptId { get; set; }
    public int? ManagerId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual Role Role { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
    public virtual User? Manager { get; set; }
    public virtual ICollection<User> Subordinates { get; set; } = new List<User>();
    public virtual ICollection<Department> ManagedDepartments { get; set; } = new List<Department>();
    public virtual ICollection<POHeader> CreatedPurchaseOrders { get; set; } = new List<POHeader>();
    public virtual ICollection<POApprovalHistory> ApprovalHistory { get; set; } = new List<POApprovalHistory>();
    public virtual ICollection<POAttachment> UploadedAttachments { get; set; } = new List<POAttachment>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}