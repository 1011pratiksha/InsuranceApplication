using Microsoft.EntityFrameworkCore;
using PolicyIssuance.Domain.Entities;

namespace PolicyIssuance.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<PolicyCoverage> PolicyCoverages { get; set; }

        public DbSet<PolicyRider> PolicyRiders { get; set; }

        public DbSet<PolicyDocument> PolicyDocuments { get; set; }

        public DbSet<PolicyEndorsement> PolicyEndorsements { get; set; }

        public DbSet<PolicyHistory> PolicyHistories { get; set; }

    //    public DbSet<User> Users { get; set; }

   //     public DbSet<Customer> Customers { get; set; }

   //     public DbSet<InsuranceProduct> InsuranceProducts { get; set; }

   //     public DbSet<Quote> Quotes { get; set; }

   //     public DbSet<RiderMaster> RiderMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior =
                    DeleteBehavior.NoAction;
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }
    }
}