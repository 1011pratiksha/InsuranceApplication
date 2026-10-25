using Microsoft.EntityFrameworkCore;
using Notification.domain.Entities;

namespace Notification.infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Notification.domain.Entities.Notification> Notifications { get; set; }
    }
}