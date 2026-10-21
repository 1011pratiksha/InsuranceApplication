using Microsoft.EntityFrameworkCore;
using PolicyRenewal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Policy_Renewal> PolicyRenewals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Policy_Renewal>(entity =>
            {
                entity.ToTable("policy_renewal");
                entity.Property(r => r.renewal_date).HasColumnType("date");
                entity.Property(r => r.new_expiry_date).HasColumnType("date");
                entity.Property(r => r.premium_amount).HasColumnType("decimal(15,2)");

                entity.HasIndex(r => new { r.policy_id, r.renewal_date }).IsUnique();
            });
        }
    }
}
