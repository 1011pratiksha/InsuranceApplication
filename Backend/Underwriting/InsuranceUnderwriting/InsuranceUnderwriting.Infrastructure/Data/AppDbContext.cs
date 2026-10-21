using InsuranceUnderwriting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }



        // Underwriting tables
        public DbSet<underwriting_case> underwriting_case { get; set; }
        public DbSet<medical_underwriting> medical_underwriting { get; set; } 
        public DbSet<financial_underwriting> financial_underwriting { get; set; } 
        public DbSet<risk_score> risk_score { get; set; } 
        public DbSet<underwriting_decision> underwriting_decision { get; set; } 
        public DbSet<underwriting_audit_log> underwriting_audit_log { get; set; } 



        // Testing tables
        public DbSet<Quote> Quotes { get; set; } 
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Insurance_Product> Insurance_Products { get; set; } 
        public DbSet<Employee> Employees { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<underwriting_case>()
                .HasOne(x => x.Quote)
                .WithMany()
                .HasForeignKey(x => x.quote_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<underwriting_case>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.customer_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<underwriting_case>()
                .HasOne(x => x.Insurance_Product)
                .WithMany()
                .HasForeignKey(x => x.product_id)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}
