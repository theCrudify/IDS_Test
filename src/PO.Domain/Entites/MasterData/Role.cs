// FILE LOCATION: src/PO.Domain/Entities/MasterData/Role.cs
// DESCRIPTION: Role entity for managing user roles and their approval permissions

using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Enums;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// Role entity - defines user roles and their permissions in the approval workflow
/// </summary>
public class Role : BaseEntity
{
    /// <summary>
    /// Role name (e.g., "User Purchasing", "Staff Purchasing", "Manager")
    /// </summary>
    [Required]
    [StringLength(50)]
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// Short role code (e.g., "USR", "STF", "MGR")
    /// </summary>
    [Required]
    [StringLength(10)]
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// Whether this role can create purchase orders
    /// </summary>
    public bool CanCreatePO { get; set; } = false;

    /// <summary>
    /// Whether this role can check purchase orders (Level 1 approval)
    /// </summary>
    public bool CanCheckPO { get; set; } = false;

    /// <summary>
    /// Whether this role can acknowledge purchase orders (Level 2 approval)
    /// </summary>
    public bool CanAcknowledgePO { get; set; } = false;

    /// <summary>
    /// Whether this role can approve purchase orders (Level 3 approval)
    /// </summary>
    public bool CanApprovePO { get; set; } = false;

    /// <summary>
    /// Approval level for this role (0=None, 1=Checker, 2=Acknowledge, 3=Approver)
    /// </summary>
    public ApprovalLevel ApprovalLevel { get; set; } = ApprovalLevel.None;

    /// <summary>
    /// Whether this role is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    /// <summary>
    /// Users with this role
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    /// <summary>
    /// Approval matrix entries where this role is the checker
    /// </summary>
    public virtual ICollection<ApprovalMatrix> CheckerMatrices { get; set; } = new List<ApprovalMatrix>();

    /// <summary>
    /// Approval matrix entries where this role acknowledges
    /// </summary>
    public virtual ICollection<ApprovalMatrix> AcknowledgeMatrices { get; set; } = new List<ApprovalMatrix>();

    /// <summary>
    /// Approval matrix entries where this role is the approver
    /// </summary>
    public virtual ICollection<ApprovalMatrix> ApproverMatrices { get; set; } = new List<ApprovalMatrix>();
}