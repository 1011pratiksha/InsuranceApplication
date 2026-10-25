using Microsoft.AspNetCore.Mvc;
using Notification.application.DTO;
using Notification.application.Interface;

namespace Notification.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository repository;

        public NotificationController(INotificationRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNotification(CreateNotificationDTO dto)
        {
            var result = await repository.AddNotificationAsync(dto);

            return Ok(result);
        }

        [HttpGet("GetAll/{customerId}")]
        public async Task<IActionResult> GetNotifications(int customerId)
        {
            var result = await repository.GetNotificationsAsync(customerId);

            return Ok(result);
        }

        [HttpGet("UnreadCount/{customerId}")]
        public async Task<IActionResult> GetUnreadCount(int customerId)
        {
            var result = await repository.GetUnreadCountAsync(customerId);

            return Ok(result);
        }

        [HttpPut("MarkAsRead/{customerId}/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int customerId, int notificationId)
        {
            var result = await repository.MarkAsReadAsync(notificationId, customerId);

            if (!result)
            {
                return NotFound("Notification not found.");
            }

            return Ok("Notification marked as read.");
        }

        [HttpPut("MarkAllAsRead/{customerId}")]
        public async Task<IActionResult> MarkAllAsRead(int customerId)
        {
            await repository.MarkAllAsReadAsync(customerId);

            return Ok("All notifications marked as read.");
        } 
    }
}
