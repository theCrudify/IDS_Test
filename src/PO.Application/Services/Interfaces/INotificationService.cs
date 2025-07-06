using PO.Shared.Common;
using PO.Domain.Enums;
using PO.Shared.DTOs.System;

namespace PO.Application.Services.Interfaces;

public interface INotificationService
{
    Task<ApiResult<bool>> CreateNotificationAsync(int recipientUserId, NotificationType type, string message, int? relatedEntityId = null, NotificationPriority priority = NotificationPriority.Medium);
    Task<ApiResult<bool>> MarkNotificationAsReadAsync(int notificationId);
    Task<ApiResult<List<NotificationDto>>> GetUserNotificationsAsync(int userId, bool unreadOnly = false);
}
