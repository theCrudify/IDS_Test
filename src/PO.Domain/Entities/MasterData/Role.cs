using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Enums;

namespace PO.Domain.Entities.MasterData;

public class Role : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string RoleName { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string RoleCode { get; set; } = string.Empty;

    public bool CanCreatePO { get; set; }
    public bool CanCheckPO { get; set; }
    public bool CanAcknowledgePO { get; set; }
    public bool CanApprovePO { get; set; }
    public ApprovalLevel ApprovalLevel { get; set; } = ApprovalLevel.None;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<ApprovalMatrix> CheckerMatrices { get; set; } = new List<ApprovalMatrix>();
    public virtual ICollection<ApprovalMatrix> AcknowledgeMatrices { get; set; } = new List<ApprovalMatrix>();
    public virtual ICollection<ApprovalMatrix> ApproverMatrices { get; set; } = new List<ApprovalMatrix>();
}