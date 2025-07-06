using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.MasterData;

namespace PO.Domain.Entities.PurchaseOrder;

public class POApprovalHistory : BaseEntity
{
    public int POId { get; set; }
    public ApprovalLevel ApprovalLevel { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
    public int ApproverId { get; set; }
    public DateTime ApprovalDate { get; set; } = DateTime.Now;

    [MaxLength(200)]
    public string? Comments { get; set; }

    [MaxLength(200)]
    public string? RejectionReason { get; set; }

    // Navigation properties
    public virtual POHeader POHeader { get; set; } = null!;
    public virtual User Approver { get; set; } = null!;
}