// FILE LOCATION: src/PO.Domain/Entities/PurchaseOrder/POHeader.cs
// DESCRIPTION: Purchase Order Header entity - main PO document with approval workflow

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.System;

namespace PO.Domain.Entities.PurchaseOrder;

/// <summary>
/// Purchase Order Header entity - represents the main PO document
/// </summary>
public class POHeader : BaseEntity
{
    /// <summary>
    /// KPIN PO Number (e.g., "KPIN-IMP-2506-003")
    /// </summary>
    [Required]
    [StringLength(20)]
    public string PONumber { get; set; } = string.Empty;

    /// <summary>
    /// Original SAP PO Number if different from PONumber
    /// </summary>
    [StringLength(20)]
    public string? SAPPONumber { get; set; }

    /// <summary>
    /// SAP Primary Number (e.g., "20860", "20874")
    /// </summary>
    [StringLength(20)]
    public string? SAPPrimaryNumber { get; set; }

    /// <summary>
    /// Vendor ID (foreign key to Vendor entity)
    /// </summary>
    [Required]
    [StringLength(10)]
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Date when PO is posted/created
    /// </summary>
    [Required]
    public DateTime PostingDate { get; set; }

    /// <summary>
    /// Estimated time of arrival for goods
    /// </summary>
    public DateTime? ETADate { get; set; }

    /// <summary>
    /// Document creation date
    /// </summary>
    [Required]
    public DateTime DocumentDate { get; set; }

    /// <summary>
    /// Required delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// Purchase Order type
    /// </summary>
    [Required]
    public POType POType { get; set; }

    /// <summary>
    /// Purchase Request number that originated this PO
    /// </summary>
    [StringLength(50)]
    public string? PRNumber { get; set; }

    /// <summary>
    /// Shipping method (Air, Sea, Land, etc.)
    /// </summary>
    [StringLength(50)]
    public string? ShippingType { get; set; }

    /// <summary>
    /// Currency code for this PO
    /// </summary>
    [Required]
    [StringLength(5)]
    public string CurrencyCode { get; set; } = "IDR";

    /// <summary>
    /// User who created this PO
    /// </summary>
    [Required]
    public int CreatedById { get; set; }

    /// <summary>
    /// Department that created this PO
    /// </summary>
    [Required]
    public int DeptId { get; set; }

    // Financial Details
    /// <summary>
    /// Total amount before discount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalBeforeDiscount { get; set; } = 0.00m;

    /// <summary>
    /// Discount percentage applied
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal DiscountPercent { get; set; } = 0.00m;

    /// <summary>
    /// Total discount amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; } = 0.00m;

    /// <summary>
    /// Total tax amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; } = 0.00m;

    /// <summary>
    /// Final total amount due
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalDue { get; set; } = 0.00m;

    // Approval Status Enhancement
    /// <summary>
    /// Current status of the PO
    /// </summary>
    [Required]
    public POStatus Status { get; set; } = POStatus.Draft;

    /// <summary>
    /// Current approval level (0=Draft, 1=Checking, 2=Acknowledge, 3=Approved)
    /// </summary>
    public ApprovalLevel CurrentApprovalLevel { get; set; } = ApprovalLevel.None;

    // SAP Integration Fields
    /// <summary>
    /// Status in SAP system
    /// </summary>
    [StringLength(50)]
    public string? SAPStatus { get; set; }

    /// <summary>
    /// SAP document entry number
    /// </summary>
    public int? SAPDocEntry { get; set; }

    /// <summary>
    /// Date when data was interfaced to/from SAP
    /// </summary>
    public DateTime? InterfaceDate { get; set; }

    // Remarks and Comments
    /// <summary>
    /// General remarks for the PO
    /// </summary>
    public string? Remarks { get; set; }

    /// <summary>
    /// Comments from the user who created the PO
    /// </summary>
    public string? UserComments { get; set; }

    /// <summary>
    /// Approval original field from SAP
    /// </summary>
    [StringLength(100)]
    public string? ApprovalOriginal { get; set; }

    /// <summary>
    /// Approval code field from SAP
    /// </summary>
    [StringLength(100)]
    public string? ApprovalCode { get; set; }

    // Navigation properties
    /// <summary>
    /// Vendor information
    /// </summary>
    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; } = null!;

    /// <summary>
    /// User who created this PO
    /// </summary>
    [ForeignKey("CreatedById")]
    public virtual User CreatedByUser { get; set; } = null!;

    /// <summary>
    /// Department that owns this PO
    /// </summary>
    [ForeignKey("DeptId")]
    public virtual Department Department { get; set; } = null!;

    /// <summary>
    /// Purchase order detail lines
    /// </summary>
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();

    /// <summary>
    /// Approval history for this PO
    /// </summary>
    public virtual ICollection<POApprovalHistory> ApprovalHistory { get; set; } = new List<POApprovalHistory>();

    /// <summary>
    /// File attachments for this PO
    /// </summary>
    public virtual ICollection<POAttachment> Attachments { get; set; } = new List<POAttachment>();

    /// <summary>
    /// Notifications related to this PO
    /// </summary>
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}