using Notification.application.DTO;
using Notification.domain.Entities;

namespace Notification.application.Interface
{
    public interface INotificationRepository
    {
        Task<Notification.domain.Entities.Notification> AddNotificationAsync(CreateNotificationDTO dto);
        Task<List<Notification.domain.Entities.Notification>> GetNotificationsAsync(int customerId);
        Task<int> GetUnreadCountAsync(int customerId);
        Task<bool> MarkAsReadAsync(int notificationId, int customerId);
        Task MarkAllAsReadAsync(int customerId);
    }

}
