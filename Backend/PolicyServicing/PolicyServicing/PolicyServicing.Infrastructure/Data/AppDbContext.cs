
using Microsoft.EntityFrameworkCore;
using PolicyServicing.Domain.Entities;

namespace PolicyServicing.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<PolicyEndorsement> PolicyEndorsements { get; set; }
        public DbSet<NomineeDetails> NomineeDetails { get; set; }
        public DbSet<PolicyRenewal> PolicyRenewals { get; set; }
        public DbSet<PolicyCancellation> PolicyCancellations { get; set; }
        public DbSet<ServiceRequestHistory> ServiceRequestHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceRequest>()
                .HasIndex(x => x.request_number)
                .IsUnique();

            modelBuilder.Entity<ServiceRequest>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_ServiceRequest_Status",
                    "[status] IN ('Submitted', 'Under Review', 'Approved', 'Rejected', 'Completed', 'Closed')"));

            //modelBuilder.Entity<ServiceRequest>()
            //    .HasOne(x => x.Policy)
            //    .WithMany()
            //    .HasForeignKey(x => x.policy_id)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ServiceRequest>()
            //    .HasOne(x => x.Customer)
            //    .WithMany()
            //    .HasForeignKey(x => x.customer_id)
            //    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PolicyEndorsement>()
                .HasIndex(x => x.endorsement_number)
                .IsUnique();

            modelBuilder.Entity<PolicyEndorsement>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_PolicyEndorsement_ApprovalStatus",
                    "[approval_status] IN ('Pending', 'Approved', 'Rejected')"));

            modelBuilder.Entity<PolicyEndorsement>()
                .HasOne(x => x.ServiceRequest)
                .WithMany(x => x.Endorsements)
                .HasForeignKey(x => x.request_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NomineeDetails>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_NomineeDetails_SharePercentage",
                    "[share_percentage] > 0 AND [share_percentage] <= 100"));

            modelBuilder.Entity<NomineeDetails>()
                .Property(x => x.share_percentage)
                .HasPrecision(5, 2);

            //modelBuilder.Entity<NomineeDetails>()
            //    .HasOne(x => x.Policy)
            //    .WithMany()
            //    .HasForeignKey(x => x.policy_id)
            //    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PolicyRenewal>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_PolicyRenewal_Status",
                    "[renewal_status] IN ('Pending', 'Paid', 'Renewed', 'Expired')"));

            modelBuilder.Entity<PolicyRenewal>()
                .Property(x => x.premium_amount)
                .HasPrecision(15, 2);

            //modelBuilder.Entity<PolicyRenewal>()
            //    .HasOne(x => x.Policy)
            //    .WithMany()
            //    .HasForeignKey(x => x.policy_id)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PolicyCancellation>()
                .Property(x => x.refund_amount)
                .HasPrecision(15, 2);

            //modelBuilder.Entity<PolicyCancellation>()
            //    .HasOne(x => x.Policy)
            //    .WithMany()
            //    .HasForeignKey(x => x.policy_id)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequestHistory>()
                .HasOne(x => x.ServiceRequest)
                .WithMany(x => x.History)
                .HasForeignKey(x => x.request_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
