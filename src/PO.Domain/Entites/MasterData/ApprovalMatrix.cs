// FILE LOCATION: src/PO.Domain/Entities/MasterData/ApprovalMatrix.cs
// DESCRIPTION: Approval matrix entity for defining approval workflow rules based on amount and department

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// Approval Matrix entity - defines approval workflow rules based on amount ranges and departments
/// </summary>
public class ApprovalMatrix : BaseEntity
{
    /// <summary>
    /// Department ID (null means applies to all departments)
    /// </summary>
    public int? DeptId { get; set; }

    /// <summary>
    /// Minimum amount for this approval rule (inclusive)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal MinAmount { get; set; }

    /// <summary>
    /// Maximum amount for this approval rule (inclusive)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal MaxAmount { get; set; }

    /// <summary>
    /// Role ID for checker level (Level 1 approval)
    /// </summary>
    [Required]
    public int CheckerRoleId { get; set; }

    /// <summary>
    /// Role ID for acknowledge level (Level 2 approval)
    /// </summary>
    [Required]
    public int AcknowledgeRoleId { get; set; }

    /// <summary>
    /// Role ID for approver level (Level 3 approval)
    /// </summary>
    [Required]
    public int ApproverRoleId { get; set; }

    /// <summary>
    /// Whether this approval matrix rule is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Priority order when multiple rules match (lower number = higher priority)
    /// </summary>
    public int Priority { get; set; } = 1;

    /// <summary>
    /// Additional conditions or notes for this rule
    /// </summary>
    public string? Conditions { get; set; }

    // Navigation properties
    /// <summary>
    /// Department this rule applies to (null = all departments)
    /// </summary>
    [ForeignKey("DeptId")]
    public virtual Department? Department { get; set; }

    /// <summary>
    /// Role responsible for checking (Level 1)
    /// </summary>
    [ForeignKey("CheckerRoleId")]
    public virtual Role CheckerRole { get; set; } = null!;

    /// <summary>
    /// Role responsible for acknowledging (Level 2)
    /// </summary>
    [ForeignKey("AcknowledgeRoleId")]
    public virtual Role AcknowledgeRole { get; set; } = null!;

    /// <summary>
    /// Role responsible for approving (Level 3)
    /// </summary>
    [ForeignKey("ApproverRoleId")]
    public virtual Role ApproverRole { get; set; } = null!;
}