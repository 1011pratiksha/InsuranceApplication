
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Domain.Entities;

namespace UnderwritingMS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<underwriting_case> underwriting_case
            => Set<underwriting_case>();

        public DbSet<medical_underwriting> medical_underwriting
            => Set<medical_underwriting>();

        public DbSet<financial_underwriting> financial_underwriting
            => Set<financial_underwriting>();

        public DbSet<risk_score> risk_score
            => Set<risk_score>();

        public DbSet<underwriting_decision> underwriting_decision
            => Set<underwriting_decision>();

        public DbSet<underwriting_audit_log> underwriting_audit_log
            => Set<underwriting_audit_log>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<underwriting_case>(entity =>
            {
                entity.ToTable("underwriting_case");

                entity.HasKey(x => x.underwriting_id);

                entity.HasIndex(x => x.quote_id).IsUnique();

                entity.Property(x => x.underwriting_date)
                    .HasColumnType("date");

                entity.Property(x => x.status)
                    .HasMaxLength(30)
                    .IsRequired();
            });


            modelBuilder.Entity<medical_underwriting>(entity =>
            {
                entity.ToTable("medical_underwriting");

                entity.HasKey(x => x.medical_id);

                entity.Property(x => x.height).HasPrecision(5, 2);
                entity.Property(x => x.weight).HasPrecision(5, 2);
                entity.Property(x => x.bmi).HasPrecision(5, 2);

                entity.Property(x => x.blood_pressure).HasMaxLength(50);
                entity.Property(x => x.medical_history).HasColumnType("nvarchar(max)");
                entity.Property(x => x.medical_report_path).HasMaxLength(500);

                entity.HasOne<underwriting_case>()
                    .WithMany()
                    .HasForeignKey(x => x.underwriting_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<financial_underwriting>(entity =>
            {
                entity.ToTable("financial_underwriting");

                entity.HasKey(x => x.financial_id);

                entity.Property(x => x.annual_income).HasPrecision(15, 2);
                entity.Property(x => x.monthly_income).HasPrecision(15, 2);
                entity.Property(x => x.employer_name).HasMaxLength(150);
                entity.Property(x => x.occupation).HasMaxLength(100);
                entity.Property(x => x.existing_insurance).HasPrecision(15, 2);
                entity.Property(x => x.loan_liability).HasPrecision(15, 2);

                entity.HasOne<underwriting_case>()
                    .WithMany()
                    .HasForeignKey(x => x.underwriting_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<risk_score>(entity =>
            {
                entity.ToTable("risk_score");

                entity.HasKey(x => x.risk_id);

                entity.Property(x => x.risk_category)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasOne<underwriting_case>()
                    .WithMany()
                    .HasForeignKey(x => x.underwriting_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<underwriting_decision>(entity =>
            {
                entity.ToTable("underwriting_decision");

                entity.HasKey(x => x.decision_id);

                entity.Property(x => x.decision)
                    .HasMaxLength(40)
                    .IsRequired();

                entity.Property(x => x.loading_percentage)
                    .HasPrecision(5, 2);

                entity.Property(x => x.exclusion_details)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.rejection_reason)
                    .HasColumnType("nvarchar(max)");

                entity.HasOne<underwriting_case>()
                    .WithMany()
                    .HasForeignKey(x => x.underwriting_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<underwriting_audit_log>(entity =>
            {
                entity.ToTable("underwriting_audit_log");

                entity.HasKey(x => x.audit_id);

                entity.Property(x => x.action)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.remarks)
                    .HasMaxLength(500);

                entity.HasOne<underwriting_case>()
                    .WithMany()
                    .HasForeignKey(x => x.underwriting_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
