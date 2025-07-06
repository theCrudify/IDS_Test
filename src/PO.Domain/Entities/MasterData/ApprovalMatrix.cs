using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;

namespace PO.Domain.Entities.MasterData;

public class ApprovalMatrix : BaseEntity
{
    public int DeptId { get; set; }
    public decimal MinAmount { get; set; } = 0;
    public decimal MaxAmount { get; set; } = 0;
    public int CheckerRoleId { get; set; }
    public int AcknowledgeRoleId { get; set; }
    public int ApproverRoleId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual Department Department { get; set; } = null!;
    public virtual Role CheckerRole { get; set; } = null!;
    public virtual Role AcknowledgeRole { get; set; } = null!;
    public virtual Role ApproverRole { get; set; } = null!;
}