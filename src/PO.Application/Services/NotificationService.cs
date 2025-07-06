using AutoMapper;
using PO.Application.Services.Interfaces;
using PO.Domain.Entities.System;
using PO.Domain.Enums;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;
using PO.Shared.DTOs.System;

namespace PO.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IGenericRepository<Notification> _notificationRepository;
    private readonly IMapper _mapper;

    public NotificationService(
        IGenericRepository<Notification> notificationRepository,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<ApiResult<bool>> CreateNotificationAsync(int recipientUserId, NotificationType type, string message, int? relatedEntityId = null, NotificationPriority priority = NotificationPriority.Medium)
    {
        var notification = new Notification
        {
            UserId = recipientUserId,
            Type = type,
            Message = message,
            RelatedEntityId = relatedEntityId,
            Priority = priority,
            CreatedAt = DateTime.Now,
            IsRead = false
        };

        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();

        return ApiResult<bool>.SuccessResult(true, "Notification created successfully.");
    }

    public async Task<ApiResult<bool>> MarkNotificationAsReadAsync(int notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);
        if (notification == null)
        {
            return ApiResult<bool>.ErrorResult("Notification not found.");
        }

        notification.IsRead = true;
        await _notificationRepository.UpdateAsync(notification);
        await _notificationRepository.SaveChangesAsync();

        return ApiResult<bool>.SuccessResult(true, "Notification marked as read.");
    }

    public async Task<ApiResult<List<NotificationDto>>> GetUserNotificationsAsync(int userId, bool unreadOnly = false)
    {
        var notifications = await _notificationRepository.GetAllAsync(n => n.UserId == userId && (unreadOnly ? !n.IsRead : true));
        var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);
        return ApiResult<List<NotificationDto>>.SuccessResult(notificationDtos);
    }
}
