
using ClaimManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Claim Management Models
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimAssessment> ClaimAssessments { get; set; }
        public DbSet<ClaimDocument> ClaimDocuments { get; set; }
        public DbSet<ClaimHistory> ClaimHistories { get; set; }
        public DbSet<SurveyAssignment> SurveyAssignments { get; set; }

        // Customer Model
        public DbSet<Customer> Customers { get; set; }

        // Policy Models
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyCoverage> PolicyCoverages { get; set; }

        // Product Model
        public DbSet<InsuranceProduct> InsuranceProducts { get; set; }

        // Premium Models
        public DbSet<PremiumSchedule> PremiumSchedules { get; set; }
        public DbSet<PremiumPayment> PremiumPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Claim
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("claim_master");

                entity.HasKey(e => e.ClaimId);

                entity.HasIndex(e => e.ClaimNumber)
                    .IsUnique();

                entity.Property(e => e.ClaimNumber)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(e => e.ClaimType)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.EstimatedLoss)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.ClaimStatus)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            // ClaimDocument: Claim 1 -> Many Documents
            modelBuilder.Entity<ClaimDocument>(entity =>
            {
                entity.ToTable("claim_documents");

                entity.HasKey(e => e.DocumentId);

                entity.Property(e => e.DocumentName)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.FilePath)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(e => e.Claim)
                    .WithMany()
                    .HasForeignKey(e => e.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ClaimAssessment: Claim 1 -> Many Assessments
            modelBuilder.Entity<ClaimAssessment>(entity =>
            {
                entity.ToTable("claim_assessment");

                entity.HasKey(e => e.AssessmentId);

                entity.Property(e => e.AssessedAmount)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.Deductible)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.Depreciation)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.NonPayable)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.NetPayable)
                    .HasColumnType("decimal(15,2)");

                entity.HasOne(e => e.Claim)
                    .WithMany()
                    .HasForeignKey(e => e.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ClaimHistory: Claim 1 -> Many History Records
            modelBuilder.Entity<ClaimHistory>(entity =>
            {
                entity.ToTable("claim_history");

                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.Action)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500);

                entity.HasOne(e => e.Claim)
                    .WithMany()
                    .HasForeignKey(e => e.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // SurveyAssignment: Claim 1 -> Many Surveys
            modelBuilder.Entity<SurveyAssignment>(entity =>
            {
                entity.ToTable("survey_assignment");

                entity.HasKey(e => e.SurveyId);

                entity.Property(e => e.ReportPath)
                    .HasMaxLength(500);

                entity.Property(e => e.SurveyStatus)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(e => e.Claim)
                    .WithMany()
                    .HasForeignKey(e => e.ClaimId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasKey(e => e.CustomerId);

                entity.HasIndex(e => e.CustomerCode)
                    .IsUnique();

                entity.HasIndex(e => e.MobileNumber)
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.AnnualIncome)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.CustomerStatus)
                    .HasMaxLength(20)
                    .IsRequired();
            });

            // InsuranceProduct
            modelBuilder.Entity<InsuranceProduct>(entity =>
            {
                entity.ToTable("insurance_product");

                entity.HasKey(e => e.ProductId);

                entity.HasIndex(e => e.ProductCode)
                    .IsUnique();

                entity.Property(e => e.MinimumSumInsured)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.MaximumSumInsured)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsRequired();
            });

            // Policy: Customer 1 -> Many Policies
            modelBuilder.Entity<Policy>(entity =>
            {
                entity.ToTable("policy");

                entity.HasKey(e => e.PolicyId);

                entity.HasIndex(e => e.PolicyNumber)
                    .IsUnique();

                entity.Property(e => e.PolicyStatus)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne<Customer>()
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<InsuranceProduct>()
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PolicyCoverage: Policy 1 -> Many Coverages
            modelBuilder.Entity<PolicyCoverage>(entity =>
            {
                entity.ToTable("policy_coverage");

                entity.HasKey(e => e.CoverageId);

                entity.Property(e => e.CoverageName)
                    .HasMaxLength(150);

                entity.Property(e => e.SumInsured)
                    .HasColumnType("decimal(15,2)");

                entity.HasOne<Policy>()
                    .WithMany()
                    .HasForeignKey(e => e.PolicyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PremiumSchedule: Policy 1 -> Many Schedules
            modelBuilder.Entity<PremiumSchedule>(entity =>
            {
                entity.ToTable("premium_schedule");

                entity.HasKey(e => e.ScheduleId);

                entity.Property(e => e.PremiumAmount)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.LateFee)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne<Policy>()
                    .WithMany()
                    .HasForeignKey(e => e.PolicyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PremiumPayment: Schedule 1 -> Many Payments
            modelBuilder.Entity<PremiumPayment>(entity =>
            {
                entity.ToTable("premium_payment");

                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.AmountPaid)
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.PaymentMode)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.TransactionReference)
                    .HasMaxLength(100);

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne<PremiumSchedule>()
                    .WithMany()
                    .HasForeignKey(e => e.ScheduleId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Policy>()
                    .WithMany()
                    .HasForeignKey(e => e.PolicyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

