
using Microsoft.EntityFrameworkCore;
using PremiumPayment.Domain.Entities;
using PremiumPayment.Infrastructure.Entities;

namespace PremiumPayment.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<premium_payment> premium_payment { get; set; } = null!;
        public DbSet<payment_receipt> payment_receipt { get; set; } = null!;
        public DbSet<premium_refund> premium_refund { get; set; } = null!;
        public DbSet<PremiumReminders> premium_reminder { get; set; } = null!;
        public DbSet<premium_schedule> premium_schedule { get; set; } = null!;

        // Read-only mapping to the existing Auth Microservice Users table.
        public DbSet<AuthUser> AuthUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthUser>(e =>
            {
                e.ToTable("Users", "dbo");
                e.HasKey(x => x.UserId);

                e.Property(x => x.UserId).HasColumnName("UserId");
                e.Property(x => x.Status).HasColumnName("Status");
                e.Property(x => x.TokenVersion).HasColumnName("TokenVersion");

                e.Metadata.SetIsTableExcludedFromMigrations(true);
            });

            modelBuilder.Entity<premium_payment>(e =>
            {
                e.ToTable("premium_payment", "dbo");
                e.HasKey(x => x.payment_id);

                e.Property(x => x.schedule_id).IsRequired();
                e.Property(x => x.policy_id).IsRequired();
                e.Property(x => x.payment_date).IsRequired();

                e.Property(x => x.amount_paid)
                    .HasColumnType("decimal(15,2)")
                    .IsRequired();

                e.Property(x => x.payment_mode)
                    .HasMaxLength(20)
                    .IsRequired();

                e.Property(x => x.transaction_reference)
                    .HasMaxLength(100)
                    .IsRequired();

                e.Property(x => x.payment_status)
                    .HasMaxLength(20)
                    .IsRequired();

                e.HasIndex(x => x.schedule_id)
                    .HasDatabaseName("idx_premium_payment_schedule_id");

                e.HasIndex(x => x.policy_id)
                    .HasDatabaseName("idx_premium_payment_policy_id");

                e.HasOne(x => x.payment_receipt)
                    .WithOne(x => x.premium_payment)
                    .HasForeignKey<payment_receipt>(x => x.payment_id);

                e.HasMany(x => x.premium_refunds)
                    .WithOne(x => x.premium_payment)
                    .HasForeignKey(x => x.payment_id);
            });

            modelBuilder.Entity<payment_receipt>(e =>
            {
                e.ToTable("payment_receipt", "dbo");
                e.HasKey(x => x.receipt_id);

                e.Property(x => x.payment_id).IsRequired();

                e.Property(x => x.receipt_number)
                    .HasMaxLength(50)
                    .IsRequired();

                e.Property(x => x.receipt_date).IsRequired();

                e.Property(x => x.receipt_path)
                    .HasMaxLength(500)
                    .IsRequired();

                e.HasIndex(x => x.receipt_number)
                    .IsUnique()
                    .HasDatabaseName("uk_payment_receipt_receipt_number");
            });

            modelBuilder.Entity<premium_refund>(e =>
            {
                e.ToTable("premium_refund", "dbo");
                e.HasKey(x => x.refund_id);

                e.Property(x => x.payment_id).IsRequired();

                e.Property(x => x.refund_amount)
                    .HasColumnType("decimal(15,2)")
                    .IsRequired();

                e.Property(x => x.refund_date).IsRequired();

                e.Property(x => x.refund_status)
                    .HasMaxLength(20)
                    .IsRequired();

                e.Property(x => x.approved_by)
                    .IsRequired(false);
            });

            modelBuilder.Entity<premium_schedule>(e =>
            {
                e.ToTable("premium_schedule", "dbo");
                e.HasKey(x => x.schedule_id);

                e.Property(x => x.premium_amount)
                    .HasColumnType("decimal(15,2)");

                e.Property(x => x.late_fee)
                    .HasColumnType("decimal(15,2)")
                    .HasDefaultValue(0m);

                e.Property(x => x.status)
                    .HasMaxLength(20);

                e.HasIndex(x => new
                {
                    x.policy_id,
                    x.installment_no
                })
                .IsUnique();
            });

            modelBuilder.Entity<PremiumReminders>(e =>
            {
                e.ToTable("PremiumReminders", "dbo");
                e.HasKey(x => x.reminder_id);

                e.HasOne(x => x.premium_Schedule)
                    .WithMany(x => x.premium_reminders)
                    .HasForeignKey(x => x.schedule_id);
            });
        }
    }
}
