using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.System;

namespace PO.Domain.Entities.PurchaseOrder;

public class POHeader : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string PONumber { get; set; } = string.Empty;

    public DateTime PODate { get; set; } = DateTime.Now;
    public DateTime PostingDate { get; set; } = DateTime.Now;
    public POStatus Status { get; set; } = POStatus.Draft;
    public POType POType { get; set; } = POType.Local;

    public int VendorIdInt { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string VendorId { get; set; } = string.Empty;

    public int DeptId { get; set; }
    public int CreatedById { get; set; }

    [MaxLength(200)]
    public string? Notes { get; set; }

    [MaxLength(200)]
    public string? DeliveryAddress { get; set; }

    public DateTime? DeliveryDate { get; set; }

    [MaxLength(3)]
    public string Currency { get; set; } = "IDR";

    public decimal ExchangeRate { get; set; } = 1.0m;
    public decimal SubTotal { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal TotalDue { get; set; } = 0;

    [MaxLength(100)]
    public string? ApprovalNotes { get; set; }

    public DateTime? ApprovedDate { get; set; }
    public DateTime? RejectedDate { get; set; }

    [MaxLength(200)]
    public string? RejectionReason { get; set; }

    // Navigation properties
    public virtual Vendor Vendor { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
    public virtual User CreatedByUser { get; set; } = null!;
    public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();
    public virtual ICollection<POApprovalHistory> ApprovalHistory { get; set; } = new List<POApprovalHistory>();
    public virtual ICollection<POAttachment> Attachments { get; set; } = new List<POAttachment>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}