// FILE LOCATION: src/PO.Domain/Entities/PurchaseOrder/POApprovalHistory.cs
// DESCRIPTION: PO Approval History entity - tracks the 3-level approval workflow with audit trail

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.MasterData;

namespace PO.Domain.Entities.PurchaseOrder;

/// <summary>
/// PO Approval History entity - maintains complete audit trail of approval workflow
/// </summary>
public class POApprovalHistory : BaseEntity
{
    /// <summary>
    /// Purchase Order ID (foreign key to POHeader)
    /// </summary>
    [Required]
    public int POId { get; set; }

    /// <summary>
    /// Approval level (1=Checker, 2=Acknowledge, 3=Approver)
    /// </summary>
    [Required]
    public ApprovalLevel ApprovalLevel { get; set; }

    /// <summary>
    /// User who performed the approval/rejection
    /// </summary>
    [Required]
    public int ApproverId { get; set; }

    /// <summary>
    /// Date and time when approval/rejection was performed
    /// </summary>
    [Required]
    public DateTime ApprovalDate { get; set; }

    /// <summary>
    /// Approval status (Approved or Rejected)
    /// </summary>
    [Required]
    public ApprovalStatus ApprovalStatus { get; set; }

    /// <summary>
    /// Comments/remarks from the approver
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Hash of the e-signature for digital signature verification
    /// </summary>
    [StringLength(255)]
    public string? ESignatureHash { get; set; }

    /// <summary>
    /// IP address from where the approval was made
    /// </summary>
    [StringLength(45)]
    public string? IPAddress { get; set; }

    /// <summary>
    /// User agent/browser information
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Time taken to make the decision (in minutes)
    /// </summary>
    public int? DecisionTimeMinutes { get; set; }

    /// <summary>
    /// Whether this was an escalated approval
    /// </summary>
    public bool IsEscalated { get; set; } = false;

    /// <summary>
    /// Reason for escalation if applicable
    /// </summary>
    [StringLength(500)]
    public string? EscalationReason { get; set; }

    // Navigation properties
    /// <summary>
    /// Purchase Order that this approval relates to
    /// </summary>
    [ForeignKey("POId")]
    public virtual POHeader POHeader { get; set; } = null!;

    /// <summary>
    /// User who performed the approval
    /// </summary>
    [ForeignKey("ApproverId")]
    public virtual User Approver { get; set; } = null!;
}