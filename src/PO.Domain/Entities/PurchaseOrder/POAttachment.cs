using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Entities.MasterData;

namespace PO.Domain.Entities.PurchaseOrder;

public class POAttachment : BaseEntity
{
    public int POId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string FilePath { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? FileType { get; set; }

    public long FileSize { get; set; } = 0;

    [MaxLength(200)]
    public string? Description { get; set; }

    public int UploadedBy { get; set; }
    public DateTime UploadedDate { get; set; } = DateTime.Now;

    // Navigation properties
    public virtual POHeader POHeader { get; set; } = null!;
    public virtual User UploadedByUser { get; set; } = null!;
}