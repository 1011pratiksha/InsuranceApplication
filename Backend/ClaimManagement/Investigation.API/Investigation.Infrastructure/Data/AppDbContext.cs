using Investigation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Investigation.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<InvestigationCase> InvestigationCases { get; set; }
        public DbSet<InvestigationHistory> InvestigationHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InvestigationCase>(entity =>
            {
                entity.ToTable("investigation");

                entity.HasKey(e => e.InvestigationId);

                entity.Property(e => e.InvestigationReason)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.ReportPath)
                    .HasMaxLength(500);

                entity.Property(e => e.InvestigationStatus)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            modelBuilder.Entity<InvestigationHistory>(entity =>
            {
                entity.ToTable("investigation_history");

                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.Action)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.PerformedBy)
                    .HasMaxLength(100);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500);

                entity.HasOne(e => e.InvestigationCase)
                    .WithMany(e => e.History)
                    .HasForeignKey(e => e.InvestigationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}