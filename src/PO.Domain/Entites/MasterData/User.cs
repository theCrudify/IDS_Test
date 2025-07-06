// FILE LOCATION: src/PO.Domain/Entities/MasterData/User.cs
// DESCRIPTION: User entity for managing system users with their roles and approval limits

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;
using PO.Domain.Entities.System;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// User entity - represents system users with their authentication and approval information
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Unique employee code from HR system
    /// </summary>
    [Required]
    [StringLength(20)]
    public string EmployeeCode { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the user
    /// </summary>
    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email address for notifications and login
    /// </summary>
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hashed password for authentication
    /// </summary>
    [StringLength(255)]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Role ID (foreign key to Role entity)
    /// </summary>
    [Required]
    public int RoleId { get; set; }

    /// <summary>
    /// Department ID (foreign key to Department entity)
    /// </summary>
    [Required]
    public int DeptId { get; set; }

    /// <summary>
    /// Manager ID (foreign key to User entity - self reference)
    /// </summary>
    public int? ManagerId { get; set; }

    /// <summary>
    /// Whether this user account is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Maximum amount this user can approve (in IDR)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal ApprovalLimit { get; set; } = 0.00m;

    /// <summary>
    /// Digital signature data for e-sign functionality
    /// </summary>
    public string? ESignature { get; set; }

    /// <summary>
    /// Last login timestamp
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Phone number for notifications
    /// </summary>
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    // Navigation properties
    /// <summary>
    /// User's role
    /// </summary>
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;

    /// <summary>
    /// User's department
    /// </summary>
    [ForeignKey("DeptId")]
    public virtual Department Department { get; set; } = null!;

    /// <summary>
    /// User's manager (self-reference)
    /// </summary>
    [ForeignKey("ManagerId")]
    public virtual User? Manager { get; set; }

    /// <summary>
    /// Users managed by this user
    /// </summary>
    public virtual ICollection<User> Subordinates { get; set; } = new List<User>();

    /// <summary>
    /// Purchase orders created by this user
    /// </summary>
    public virtual ICollection<POHeader> CreatedPurchaseOrders { get; set; } = new List<POHeader>();

    /// <summary>
    /// Approval history entries by this user
    /// </summary>
    public virtual ICollection<POApprovalHistory> ApprovalHistory { get; set; } = new List<POApprovalHistory>();

    /// <summary>
    /// Notifications sent to this user
    /// </summary>
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    /// <summary>
    /// File attachments uploaded by this user
    /// </summary>
    public virtual ICollection<POAttachment> UploadedAttachments { get; set; } = new List<POAttachment>();

    /// <summary>
    /// Departments where this user is the head
    /// </summary>
    public virtual ICollection<Department> ManagedDepartments { get; set; } = new List<Department>();
}