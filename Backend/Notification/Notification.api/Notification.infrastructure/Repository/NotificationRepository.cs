using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notification.application.DTO;
using Notification.application.Interface;
using Notification.infrastructure.Data;
using NotificationEntity = Notification.domain.Entities.Notification;

namespace Notification.infrastructure.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
    public NotificationRepository(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<NotificationEntity> AddNotificationAsync(CreateNotificationDTO dto)
        {
            var notification = mapper.Map<NotificationEntity>(dto);

            db.Notifications.Add(notification);
            await db.SaveChangesAsync();

            return notification;
        }

        public async Task<List<NotificationEntity>> GetNotificationsAsync(int customerId)
        {
            return await db.Notifications
                .Where(n => n.CustomerId == customerId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(int customerId)
        {
            return await db.Notifications
                .CountAsync(n => n.CustomerId == customerId && !n.IsRead);
        }

        public async Task<bool> MarkAsReadAsync(int notificationId, int customerId)
        {
            var notification = await db.Notifications
                .FirstOrDefaultAsync(n =>
                    n.NotificationId == notificationId &&
                    n.CustomerId == customerId);

            if (notification == null)
            {
                return false;
            }

            notification.IsRead = true;
            await db.SaveChangesAsync();

            return true;
        }

        public async Task MarkAllAsReadAsync(int customerId)
        {
            var notifications = await db.Notifications
                .Where(n => n.CustomerId == customerId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await db.SaveChangesAsync();
        }
    }
}
