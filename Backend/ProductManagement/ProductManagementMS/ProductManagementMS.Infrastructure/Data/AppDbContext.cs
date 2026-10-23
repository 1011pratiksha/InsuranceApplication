using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<product_category> product_category => Set<product_category>();
        public DbSet<insurance_product> insurance_product => Set<insurance_product>();
        public DbSet<coverage_master> coverage_master => Set<coverage_master>();
        public DbSet<rider_master> rider_master => Set<rider_master>();
        public DbSet<benefit_master> benefit_master => Set<benefit_master>();
        public DbSet<premium_frequency> premium_frequency => Set<premium_frequency>();
        public DbSet<product_commission> product_commission => Set<product_commission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product Category
            modelBuilder.Entity<product_category>(entity =>
            {
                entity.ToTable("product_category");

                entity.HasKey(x => x.category_id);

                entity.Property(x => x.category_id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.category_name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(x => x.category_name)
                    .IsUnique();

                entity.Property(x => x.description)
                    .HasMaxLength(255);

                entity.Property(x => x.status)
                    .HasMaxLength(20)
                    .IsRequired();
            });

            // Insurance Product
            modelBuilder.Entity<insurance_product>(entity =>
            {
                entity.ToTable("insurance_product");

                entity.HasKey(x => x.product_id);

                entity.Property(x => x.product_id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.product_code)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.HasIndex(x => x.product_code)
                    .IsUnique();

                entity.Property(x => x.product_name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.HasIndex(x => x.product_name)
                    .IsUnique();

                entity.Property(x => x.product_type)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.description)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.minimum_sum_insured)
                    .HasPrecision(15, 2);

                entity.Property(x => x.maximum_sum_insured)
                    .HasPrecision(15, 2);

                entity.Property(x => x.status)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasOne<product_category>()
                    .WithMany()
                    .HasForeignKey(x => x.category_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Coverage Master
            modelBuilder.Entity<coverage_master>(entity =>
            {
                entity.ToTable("coverage_master");

                entity.HasKey(x => x.coverage_id);

                entity.Property(x => x.coverage_id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.coverage_name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.minimum_coverage)
                    .HasPrecision(15, 2);

                entity.Property(x => x.maximum_coverage)
                    .HasPrecision(15, 2);

                entity.Property(x => x.deductible_amount)
                    .HasPrecision(15, 2);

                entity.HasOne<insurance_product>()
                    .WithMany()
                    .HasForeignKey(x => x.product_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Rider Master
            modelBuilder.Entity<rider_master>(entity =>
            {
                entity.ToTable("rider_master");

                entity.HasKey(x => x.rider_id);

                entity.Property(x => x.rider_id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.rider_name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.rider_description)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.rider_premium)
                    .HasPrecision(15, 2);

                entity.Property(x => x.status)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasOne<insurance_product>()
                    .WithMany()
                    .HasForeignKey(x => x.product_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Benefit Master
            modelBuilder.Entity<benefit_master>(entity =>
            {
                entity.ToTable("benefit_master");

                entity.HasKey(x => x.benefit_id);

                entity.Property(x => x.benefit_id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.benefit_name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.benefit_description)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.benefit_limit)
                    .HasPrecision(15, 2);

                entity.HasOne<insurance_product>()
                    .WithMany()
                    .HasForeignKey(x => x.product_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Premium Frequency
            modelBuilder.Entity<premium_frequency>(entity =>
            {
                entity.ToTable("premium_frequency");

                entity.HasKey(x => x.frequency_id);

                entity.Property(x => x.frequency_id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.frequency_name)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.HasIndex(x => x.frequency_name)
                    .IsUnique();

                entity.Property(x => x.installment_count)
                    .IsRequired();

                entity.Property(x => x.status)
                    .HasMaxLength(20)
                    .IsRequired();
            });

            // Product Commission
            modelBuilder.Entity<product_commission>(entity =>
            {
                entity.ToTable("product_commission");

                entity.HasKey(x => x.commission_id);

                entity.Property(x => x.commission_id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.first_year_commission)
                    .HasPrecision(5, 2);

                entity.Property(x => x.renewal_commission)
                    .HasPrecision(5, 2);

                entity.Property(x => x.branch_commission)
                    .HasPrecision(5, 2);

                entity.HasIndex(x => x.product_id)
                    .IsUnique();

                entity.HasOne<insurance_product>()
                    .WithMany()
                    .HasForeignKey(x => x.product_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
