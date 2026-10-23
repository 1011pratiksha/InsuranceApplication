
using Microsoft.EntityFrameworkCore;
using Support.Domain.Entities;

namespace Support.Infrastructure.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //public DbSet<Customer> Customers { get; set; }
    public DbSet<SupportTicket> SupportTickets { get; set; }
    public DbSet<SupportResponse> SupportResponses { get; set; }
    public DbSet<SupportCategory> SupportCategories { get; set; }
    public DbSet<SupportAttachment> SupportAttachments { get; set; }
    public DbSet<SupportHistory> SupportHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // One SupportCategory can have many SupportTickets
      modelBuilder.Entity<SupportTicket>()
          .HasOne<SupportCategory>()
          .WithMany()
          .HasForeignKey(t => t.CategoryId)
          .OnDelete(DeleteBehavior.Restrict);

      // One SupportTicket can have many SupportResponses
      modelBuilder.Entity<SupportResponse>()
          .HasOne<SupportTicket>()
          .WithMany()
          .HasForeignKey(r => r.TicketId)
          .OnDelete(DeleteBehavior.Cascade);

      // One SupportTicket can have many SupportAttachments
      modelBuilder.Entity<SupportAttachment>()
          .HasOne<SupportTicket>()
          .WithMany()
          .HasForeignKey(a => a.TicketId)
          .OnDelete(DeleteBehavior.Cascade);

      // One SupportTicket can have many SupportHistory records
      modelBuilder.Entity<SupportHistory>()
          .HasOne<SupportTicket>()
          .WithMany()
          .HasForeignKey(h => h.TicketId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
