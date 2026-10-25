
using KycVerification.Domain.Entities;
using KYCVerification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KycVerification.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Customer information.
        public DbSet<Customers> Customers { get; set; }

        // Main KYC verification requests.
        public DbSet<CustomerKyc> CustomerKycs { get; set; }

        // Documents uploaded for KYC verification.
        public DbSet<KycDocument> KycDocuments { get; set; }

        // Central KYC verification details.
        public DbSet<CkycDetails> CkycDetails { get; set; }

        // KYC activity and audit history.
        public DbSet<KycVerificationLog> KycVerificationLogs { get; set; }

        // Supported document types.
        public DbSet<DocumentTypeMaster> DocumentTypeMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer code must be unique.
            modelBuilder.Entity<Customers>()
                .HasIndex(c => c.CustomerCode)
                .IsUnique();

            // Each KYC request must have a unique KYC number.
            modelBuilder.Entity<CustomerKyc>()
                .HasIndex(k => k.KycNumber)
                .IsUnique();

            // One customer can have multiple KYC requests.
            modelBuilder.Entity<CustomerKyc>()
                .HasOne<Customers>()
                .WithMany()
                .HasForeignKey(k => k.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // One KYC request can have multiple documents.
            modelBuilder.Entity<KycDocument>()
                .HasOne<CustomerKyc>()
                .WithMany()
                .HasForeignKey(d => d.KycId)
                .OnDelete(DeleteBehavior.Restrict);

            // Link CKYC details to a customer.
            modelBuilder.Entity<CkycDetails>()
                .HasOne<Customers>()
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // One KYC request can have multiple audit log entries.
            modelBuilder.Entity<KycVerificationLog>()
                .HasOne<CustomerKyc>()
                .WithMany()
                .HasForeignKey(l => l.KycId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
