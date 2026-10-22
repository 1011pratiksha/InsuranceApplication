using ClaimManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClaimMaster> ClaimMasters { get; set; }
        public DbSet<ClaimAssessment> ClaimAssessments { get; set; }
        public DbSet<ClaimDocument> ClaimDocuments { get; set; }
        public DbSet<ClaimHistory> ClaimHistories { get; set; }
        public DbSet<ClaimSettlement> ClaimSettlements { get; set; }
        public DbSet<Investigation> InvestigationCases { get; set; }
        public DbSet<SurveyAssignment> SurveyAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ClaimDocument → ClaimMaster (One-to-Many)
            modelBuilder.Entity<ClaimDocument>()
                .HasOne(d => d.ClaimMaster)
                .WithMany()
                .HasForeignKey(d => d.claim_id)
                .OnDelete(DeleteBehavior.Cascade);

            // ClaimHistory → ClaimMaster (One-to-Many)
            modelBuilder.Entity<ClaimHistory>()
                .HasOne(h => h.ClaimMaster)
                .WithMany()
                .HasForeignKey(h => h.claim_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Investigation → ClaimMaster (One-to-Many)
            modelBuilder.Entity<Investigation>()
                .HasOne(i => i.ClaimMaster)
                .WithMany()
                .HasForeignKey(i => i.claim_id)
                .OnDelete(DeleteBehavior.Cascade);

            // SurveyAssignment → ClaimMaster (One-to-Many)
            modelBuilder.Entity<SurveyAssignment>()
                .HasOne(s => s.ClaimMaster)
                .WithMany()
                .HasForeignKey(s => s.claim_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
