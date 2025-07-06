// FILE LOCATION: src/PO.Domain/Entities/PurchaseOrder/POAttachment.cs
// DESCRIPTION: PO Attachment entity - manages file attachments for purchase orders

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Entities.MasterData;

namespace PO.Domain.Entities.PurchaseOrder;

/// <summary>
/// PO Attachment entity - represents file attachments for purchase orders
/// </summary>
public class POAttachment : BaseEntity
{
    /// <summary>
    /// Purchase Order ID (foreign key to POHeader)
    /// </summary>
    [Required]
    public int POId { get; set; }

    /// <summary>
    /// Original filename as uploaded by user
    /// </summary>
    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Physical file path on server storage
    /// </summary>
    [Required]
    [StringLength(500)]
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// File type/extension (e.g., ".pdf", ".xlsx", ".jpg")
    /// </summary>
    [Required]
    [StringLength(50)]
    public string FileType { get; set; } = string.Empty;

    /// <summary>
    /// File size in bytes
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// MIME type of the file
    /// </summary>
    [StringLength(100)]
    public string? MimeType { get; set; }

    /// <summary>
    /// User who uploaded this attachment
    /// </summary>
    [Required]
    public int UploadedBy { get; set; }

    /// <summary>
    /// Date and time when file was uploaded
    /// </summary>
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Whether this attachment is currently active (not deleted)
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Description or purpose of this attachment
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Category of attachment (Quote, Specification, Contract, etc.)
    /// </summary>
    [StringLength(50)]
    public string? Category { get; set; }

    /// <summary>
    /// Version number if this is a versioned document
    /// </summary>
    [StringLength(10)]
    public string? Version { get; set; }

    /// <summary>
    /// Hash of the file content for integrity verification
    /// </summary>
    [StringLength(255)]
    public string? FileHash { get; set; }

    // Navigation properties
    /// <summary>
    /// Purchase Order that this attachment belongs to
    /// </summary>
    [ForeignKey("POId")]
    public virtual POHeader POHeader { get; set; } = null!;

    /// <summary>
    /// User who uploaded this attachment
    /// </summary>
    [ForeignKey("UploadedBy")]
    public virtual User UploadedByUser { get; set; } = null!;
}