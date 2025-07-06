// FILE LOCATION: src/PO.Domain/Entities/System/Notification.cs
// DESCRIPTION: Notification entity - manages system notifications for users about PO status changes

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PO.Domain.Common;
using PO.Domain.Enums;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.PurchaseOrder;

namespace PO.Domain.Entities.System;

/// <summary>
/// Notification entity - represents system notifications sent to users
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>
    /// User who should receive this notification
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Type of notification
    /// </summary>
    [Required]
    public NotificationType NotificationType { get; set; }

    /// <summary>
    /// Notification title/subject
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed notification message
    /// </summary>
    [Required]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Related Purchase Order ID (if applicable)
    /// </summary>
    public int? RelatedPOId { get; set; }

    /// <summary>
    /// Whether the notification has been read
    /// </summary>
    public bool IsRead { get; set; } = false;

    /// <summary>
    /// Priority level of the notification
    /// </summary>
    public NotificationPriority Priority { get; set; } = NotificationPriority.Medium;

    /// <summary>
    /// Date and time when notification was created
    /// </summary>
    public DateTime NotificationCreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when notification was read
    /// </summary>
    public DateTime? ReadAt { get; set; }

    /// <summary>
    /// Whether email notification was sent
    /// </summary>
    public bool EmailSent { get; set; } = false;

    /// <summary>
    /// Date when email was sent
    /// </summary>
    public DateTime? EmailSentAt { get; set; }

    /// <summary>
    /// Whether SMS notification was sent
    /// </summary>
    public bool SMSSent { get; set; } = false;

    /// <summary>
    /// Date when SMS was sent
    /// </summary>
    public DateTime? SMSSentAt { get; set; }

    /// <summary>
    /// URL to navigate to when notification is clicked
    /// </summary>
    [StringLength(500)]
    public string? ActionUrl { get; set; }

    /// <summary>
    /// Additional data in JSON format
    /// </summary>
    public string? AdditionalData { get; set; }

    /// <summary>
    /// Expiry date for the notification
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    // Navigation properties
    /// <summary>
    /// User who should receive this notification
    /// </summary>
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Related Purchase Order (if applicable)
    /// </summary>
    [ForeignKey("RelatedPOId")]
    public virtual POHeader? RelatedPO { get; set; }
}