using PO.Domain.Enums;

namespace PO.Shared.DTOs.System;

public class NotificationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? RelatedEntityId { get; set; }
    public NotificationPriority Priority { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsRead { get; set; }
}
