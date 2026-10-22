using InsuranceAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        //My Actual Models
        public DbSet<Agent> Agents { get; set; }

        public DbSet<AgentCustomer> AgentCustomers { get; set; }

        public DbSet<AgentPolicy> AgentPolicies { get; set; }

        public DbSet<CommissionRule> CommissionRules { get; set; }

        public DbSet<AgentCommission> AgentCommissions { get; set; }

        public DbSet<AgentCommissionPayment> AgentCommissionPayments { get; set; }


        //For Use Forgin keys

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Product> Products { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Agent -> User
            modelBuilder.Entity<Agent>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentCustomer -> Agent
            modelBuilder.Entity<AgentCustomer>()
                .HasOne(ac => ac.Agent)
                .WithMany()
                .HasForeignKey(ac => ac.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentCustomer -> Customer
            modelBuilder.Entity<AgentCustomer>()
                .HasOne(ac => ac.Customer)
                .WithMany()
                .HasForeignKey(ac => ac.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentPolicy -> Agent
            modelBuilder.Entity<AgentPolicy>()
                .HasOne(ap => ap.Agent)
                .WithMany()
                .HasForeignKey(ap => ap.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentPolicy -> Policy
            modelBuilder.Entity<AgentPolicy>()
                .HasOne(ap => ap.Policy)
                .WithMany()
                .HasForeignKey(ap => ap.PolicyId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentPolicy -> Customer
            modelBuilder.Entity<AgentPolicy>()
                .HasOne(ap => ap.Customer)
                .WithMany()
                .HasForeignKey(ap => ap.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // CommissionRule -> Product
            modelBuilder.Entity<CommissionRule>()
                .HasOne(cr => cr.Product)
                .WithMany()
                .HasForeignKey(cr => cr.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentCommission -> Agent
            modelBuilder.Entity<AgentCommission>()
                .HasOne(ac => ac.Agent)
                .WithMany()
                .HasForeignKey(ac => ac.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentCommission -> Policy
            modelBuilder.Entity<AgentCommission>()
                .HasOne(ac => ac.Policy)
                .WithMany()
                .HasForeignKey(ac => ac.PolicyId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgentCommissionPayment -> Agent
            modelBuilder.Entity<AgentCommissionPayment>()
                .HasOne(acp => acp.Agent)
                .WithMany()
                .HasForeignKey(acp => acp.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Policy -> Customer
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            // Policy -> Product
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
