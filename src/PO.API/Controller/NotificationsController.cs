using Microsoft.AspNetCore.Mvc;
using PO.Application.Services.Interfaces;

namespace PO.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : BaseController
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserNotifications(int userId, [FromQuery] bool unreadOnly = false)
    {
        var result = await _notificationService.GetUserNotificationsAsync(userId, unreadOnly);
        return HandleResult(result);
    }

    [HttpPut("{notificationId}/read")]
    public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
    {
        var result = await _notificationService.MarkNotificationAsReadAsync(notificationId);
        return HandleResult(result);
    }
}
