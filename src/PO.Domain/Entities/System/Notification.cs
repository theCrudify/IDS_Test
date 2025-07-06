using System.ComponentModel.DataAnnotations;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.System;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public NotificationType Type { get; set; }
    public NotificationPriority Priority { get; set; } = NotificationPriority.Medium;

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Message { get; set; }

    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public int? RelatedEntityId { get; set; }

    [MaxLength(200)]
    public string? ActionUrl { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual POHeader? RelatedEntity { get; set; }
}