using Microsoft.EntityFrameworkCore;
using PremiumSchedule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumSchedule.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PremiumScheduler> PremiumSchedulers {get; set;}
        public DbSet<PremiumReminder> PremiumReminders {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between PremiumScheduler and PremiumReminder
            modelBuilder.Entity<PremiumReminder>()
                            .HasOne(pr => pr.PremiumScheduler)
                            .WithMany(ps => ps.PremiumReminders)
                            .HasForeignKey(pr => pr.scheduleId)
                            .OnDelete(DeleteBehavior.Cascade);

            // One policy cannot have the same installment number twice
            modelBuilder.Entity<PremiumScheduler>()
                .HasIndex(s => new { s.policyId, s.installmentNo })
                .IsUnique();
        }
    }
}
