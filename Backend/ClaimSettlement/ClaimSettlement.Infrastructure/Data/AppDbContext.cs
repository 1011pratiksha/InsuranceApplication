using ClaimSettlement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SettlementEntity = ClaimSettlement.Domain.Entities.ClaimSettlement;

namespace ClaimSettlement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<ClaimMaster> ClaimMasters { get; set; }

        public DbSet<ClaimAssessment> ClaimAssessments { get; set; }

        public DbSet<SettlementEntity> ClaimSettlements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            modelBuilder.Entity<Customer>()
                .ToTable("customer");

            modelBuilder.Entity<Customer>()
                .HasKey(x => x.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(x => x.CustomerId)
                .HasColumnName("customer_id")
                .ValueGeneratedNever();

            modelBuilder.Entity<Customer>()
                .Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.DateOfBirth)
                .HasColumnName("date_of_birth")
                .HasColumnType("date");

            modelBuilder.Entity<Customer>()
                .Property(x => x.Gender)
                .HasColumnName("gender")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.MaritalStatus)
                .HasColumnName("marital_status")
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.MobileNumber)
                .HasColumnName("mobile_number")
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.EmailAddress)
                .HasColumnName("email_address")
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.Occupation)
                .HasColumnName("occupation")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.AnnualIncome)
                .HasColumnName("annual_income")
                .HasPrecision(15, 2);

            modelBuilder.Entity<Customer>()
                .Property(x => x.CustomerStatus)
                .HasColumnName("customer_status")
                .HasMaxLength(20)
                .IsRequired();


            modelBuilder.Entity<Policy>()
                .ToTable("policy");

            modelBuilder.Entity<Policy>()
                .HasKey(x => x.PolicyId);

            modelBuilder.Entity<Policy>()
                .Property(x => x.PolicyId)
                .HasColumnName("policy_id")
                .ValueGeneratedNever();

            modelBuilder.Entity<Policy>()
                .Property(x => x.PolicyNumber)
                .HasColumnName("policy_number")
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Policy>()
                .HasIndex(x => x.PolicyNumber)
                .IsUnique();

            modelBuilder.Entity<Policy>()
                .Property(x => x.CustomerId)
                .HasColumnName("customer_id");

            modelBuilder.Entity<Policy>()
                .Property(x => x.PolicyStatus)
                .HasColumnName("policy_status")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Policy>()
                .Property(x => x.SumInsured)
                .HasColumnName("sum_insured")
                .HasPrecision(15, 2);

            modelBuilder.Entity<Policy>()
                .Property(x => x.PolicyExpiryDate)
                .HasColumnName("policy_expiry_date")
                .HasColumnType("date");


            modelBuilder.Entity<ClaimMaster>()
                .ToTable("claim_master");

            modelBuilder.Entity<ClaimMaster>()
                .HasKey(x => x.ClaimId);

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.ClaimId)
                .HasColumnName("claim_id")
                .ValueGeneratedNever();

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.ClaimNumber)
                .HasColumnName("claim_number")
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<ClaimMaster>()
                .HasIndex(x => x.ClaimNumber)
                .IsUnique();

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.PolicyId)
                .HasColumnName("policy_id");

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.CustomerId)
                .HasColumnName("customer_id");

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.ClaimType)
                .HasColumnName("claim_type")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.IncidentDate)
                .HasColumnName("incident_date")
                .HasColumnType("date");

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.ClaimDate)
                .HasColumnName("claim_date")
                .HasColumnType("date");

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.EstimatedLoss)
                .HasColumnName("estimated_loss")
                .HasPrecision(15, 2);

            modelBuilder.Entity<ClaimMaster>()
                .Property(x => x.ClaimStatus)
                .HasColumnName("claim_status")
                .HasMaxLength(30)
                .IsRequired();


            modelBuilder.Entity<ClaimAssessment>()
                .ToTable("claim_assessment");

            modelBuilder.Entity<ClaimAssessment>()
                .HasKey(x => x.AssessmentId);

            modelBuilder.Entity<ClaimAssessment>()
                .Property(x => x.AssessmentId)
                .HasColumnName("assessment_id")
                .ValueGeneratedNever();

            modelBuilder.Entity<ClaimAssessment>()
                .Property(x => x.ClaimId)
                .HasColumnName("claim_id");

            modelBuilder.Entity<ClaimAssessment>()
                .HasIndex(x => x.ClaimId)
                .IsUnique();

            modelBuilder.Entity<ClaimAssessment>()
                .Property(x => x.AssessedAmount)
                .HasColumnName("assessed_amount")
                .HasPrecision(15, 2);

            modelBuilder.Entity<ClaimAssessment>()
                .Property(x => x.Deductible)
                .HasColumnName("deductible")
                .HasPrecision(15, 2);

            modelBuilder.Entity<ClaimAssessment>()
                .Property(x => x.Depreciation)
                .HasColumnName("depreciation")
                .HasPrecision(15, 2);

            modelBuilder.Entity<ClaimAssessment>()
                .Property(x => x.NonPayable)
                .HasColumnName("non_payable")
                .HasPrecision(15, 2);

            modelBuilder.Entity<ClaimAssessment>()
                .Property(x => x.NetPayable)
                .HasColumnName("net_payable")
                .HasPrecision(15, 2);


            modelBuilder.Entity<SettlementEntity>()
                .ToTable("claim_settlement");

            modelBuilder.Entity<SettlementEntity>()
                .HasKey(x => x.SettlementId);

            modelBuilder.Entity<SettlementEntity>()
                .Property(x => x.SettlementId)
                .HasColumnName("settlement_id")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SettlementEntity>()
                .Property(x => x.ClaimId)
                .HasColumnName("claim_id");

            modelBuilder.Entity<SettlementEntity>()
                .HasIndex(x => x.ClaimId)
                .IsUnique();

            modelBuilder.Entity<SettlementEntity>()
                .Property(x => x.ApprovedAmount)
                .HasColumnName("approved_amount")
                .HasPrecision(15, 2);

            modelBuilder.Entity<SettlementEntity>()
                .Property(x => x.PaymentMode)
                .HasColumnName("payment_mode")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<SettlementEntity>()
                .Property(x => x.TransactionReference)
                .HasColumnName("transaction_reference")
                .HasMaxLength(100);

            modelBuilder.Entity<SettlementEntity>()
                .Property(x => x.SettlementDate)
                .HasColumnName("settlement_date")
                .HasColumnType("datetime");

            modelBuilder.Entity<SettlementEntity>()
                .Property(x => x.SettlementStatus)
                .HasColumnName("settlement_status")
                .HasMaxLength(20)
                .HasDefaultValue("Pending")
                .IsRequired();
        }
    }
}