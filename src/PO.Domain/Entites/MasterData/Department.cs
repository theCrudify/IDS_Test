// FILE LOCATION: src/PO.Domain/Entities/MasterData/Department.cs
// DESCRIPTION: Department entity for organizing users and purchase orders by department

using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.MasterData;

/// <summary>
/// Department entity - represents organizational departments within the company
/// </summary>
public class Department : BaseEntity
{
    /// <summary>
    /// Short department code (e.g., "TECH", "PROD", "PURCH")
    /// </summary>
    [Required]
    [StringLength(20)]
    public string DeptCode { get; set; } = string.Empty;

    /// <summary>
    /// Full department name (e.g., "Technical Department", "Production Department")
    /// </summary>
    [Required]
    [StringLength(100)]
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// ID of the department head (references User entity)
    /// </summary>
    public int? DeptHeadId { get; set; }

    /// <summary>
    /// Whether this department is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    /// <summary>
    /// Department head user
    /// </summary>
    public virtual User? DeptHead { get; set; }

    /// <summary>
    /// Users in this department
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    /// <summary>
    /// Purchase orders from this department
    /// </summary>
    public virtual ICollection<POHeader> PurchaseOrders { get; set; } = new List<POHeader>();

    /// <summary>
    /// Approval matrix entries specific to this department
    /// </summary>
    public virtual ICollection<ApprovalMatrix> ApprovalMatrices { get; set; } = new List<ApprovalMatrix>();
}