using Microsoft.EntityFrameworkCore;
using CustomerRegistration.Domain.Entities;
using System.Linq;

namespace CustomerRegistration.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<Nominee> Nominees { get; set; }
        public DbSet<CustomerDocument> CustomerDocuments { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<InsuranceProduct> InsuranceProducts { get; set; }
        //public DbSet<Quote> Quotes { get; set; }
        //public DbSet<RiderMaster> RiderMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(c => c.CustomerCode).IsUnique();
                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.MobileNumber).IsUnique();
            });

            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }
    }
}