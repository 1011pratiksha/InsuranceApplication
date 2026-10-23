
using Microsoft.EntityFrameworkCore;

using ReportsAndDashboard.Domain.Entities;
using ReportsAndDashboard.Domain.Entities.Reporting;

namespace InsuranceApp.Infrastructure.Persistence
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Reporting DbSets

    public DbSet<ReportMaster> ReportMasters => Set<ReportMaster>();

    public DbSet<ReportSchedule> ReportSchedules => Set<ReportSchedule>();

    public DbSet<ReportExecution> ReportExecutions => Set<ReportExecution>();

    public DbSet<DashboardWidget> DashboardWidgets => Set<DashboardWidget>();

    public DbSet<AnalyticsSnapshot> AnalyticsSnapshots => Set<AnalyticsSnapshot>();

    // Customer-related DbSets

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();

    public DbSet<CustomerKyc> CustomerKycs => Set<CustomerKyc>();

    public DbSet<CustomerDocument> CustomerDocuments => Set<CustomerDocument>();

    public DbSet<CustomerNominee> CustomerNominees => Set<CustomerNominee>();

    // Policy and product DbSets

    public DbSet<Policy> Policies => Set<Policy>();

    public DbSet<InsuranceProduct> InsuranceProducts => Set<InsuranceProduct>();

    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

    // Master data DbSets

    public DbSet<Gender> Genders => Set<Gender>();

    public DbSet<MaritalStatus> MaritalStatuses => Set<MaritalStatus>();

    public DbSet<Occupation> Occupations => Set<Occupation>();

    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();

    public DbSet<Relationship> Relationships => Set<Relationship>();

    public DbSet<Country> Countries => Set<Country>();

    public DbSet<State> States => Set<State>();

    public DbSet<City> Cities => Set<City>();

    public DbSet<PremiumSchedule> PremiumSchedules { get; set; }

    public DbSet<PremiumPayment> PremiumPayments { get; set; }
    public DbSet<Claim> Claims { get; set; }

    public DbSet<UnderwritingCase> UnderwritingCases { get; set; }
    public DbSet<Agent> Agents { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<ReportMaster>(entity =>
      {
        entity.ToTable("report_master");

        entity.HasKey(x => x.ReportId);

        entity.Property(x => x.ReportId)
            .HasColumnName("report_id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.ReportName)
            .HasColumnName("report_name")
            .HasMaxLength(150)
            .IsRequired();

        entity.Property(x => x.ReportType)
            .HasColumnName("report_type")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        entity.Property(x => x.CreatedDate)
            .HasColumnName("created_date")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
      });


      modelBuilder.Entity<ReportSchedule>(entity =>
      {
        entity.ToTable("report_schedule");

        entity.HasKey(x => x.ScheduleId);

        entity.Property(x => x.ScheduleId)
            .HasColumnName("schedule_id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.ReportId)
            .HasColumnName("report_id");

        entity.Property(x => x.ScheduleType)
            .HasColumnName("schedule_type")
            .HasConversion<string>()
            .HasMaxLength(20);

        entity.Property(x => x.NextRunDate)
            .HasColumnName("next_run_date");

        entity.Property(x => x.EmailTo)
            .HasColumnName("email_to")
            .HasMaxLength(250);

        entity.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20);

        entity.HasOne(x => x.Report)
            .WithMany(x => x.Schedules)
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<ReportExecution>(entity =>
      {
        entity.ToTable("report_execution");

        entity.HasKey(x => x.ExecutionId);

        entity.Property(x => x.ExecutionId)
            .HasColumnName("execution_id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.ReportId)
            .HasColumnName("report_id");

        entity.Property(x => x.ExecutedBy)
            .HasColumnName("executed_by");

        entity.Property(x => x.ExecutionDate)
            .HasColumnName("execution_date");

        entity.Property(x => x.ExecutionStatus)
            .HasColumnName("execution_status")
            .HasConversion<string>()
            .HasMaxLength(20);

        entity.Property(x => x.FilePath)
            .HasColumnName("file_path")
            .HasMaxLength(500);

        entity.HasOne(x => x.Report)
            .WithMany(x => x.Executions)
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.Restrict);
      });


      modelBuilder.Entity<DashboardWidget>(entity =>
      {
        entity.ToTable("dashboard_widget");

        entity.HasKey(x => x.WidgetId);

        entity.Property(x => x.WidgetId)
            .HasColumnName("widget_id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.WidgetName)
            .HasColumnName("widget_name")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.WidgetType)
            .HasColumnName("widget_type")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.SequenceNo)
            .HasColumnName("sequence_no");

        entity.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20);
      });


      modelBuilder.Entity<AnalyticsSnapshot>(entity =>
      {
        entity.ToTable("analytics_snapshot");

        entity.HasKey(x => x.SnapshotId);

        entity.Property(x => x.SnapshotId)
            .HasColumnName("snapshot_id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.SnapshotDate)
            .HasColumnName("snapshot_date")
            .HasColumnType("date");

        entity.Property(x => x.TotalCustomers)
            .HasColumnName("total_customers");

        entity.Property(x => x.TotalPolicies)
            .HasColumnName("total_policies");

        entity.Property(x => x.PremiumCollected)
            .HasColumnName("premium_collected")
            .HasPrecision(15, 2);

        entity.Property(x => x.ClaimsPaid)
            .HasColumnName("claims_paid")
            .HasPrecision(15, 2);

        entity.Property(x => x.Revenue)
            .HasColumnName("revenue")
            .HasPrecision(15, 2);
      });


      modelBuilder.Entity<Customer>()
          .HasOne(x => x.Gender)
          .WithMany(x => x.Customers)
          .HasForeignKey(x => x.GenderId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Customer>()
          .HasOne(x => x.MaritalStatus)
          .WithMany(x => x.Customers)
          .HasForeignKey(x => x.MaritalStatusId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Customer>()
          .HasOne(x => x.Occupation)
          .WithMany(x => x.Customers)
          .HasForeignKey(x => x.OccupationId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Customer>()
          .HasMany(x => x.Addresses)
          .WithOne(x => x.Customer)
          .HasForeignKey(x => x.CustomerId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Customer>()
          .HasMany(x => x.KycRecords)
          .WithOne(x => x.Customer)
          .HasForeignKey(x => x.CustomerId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Customer>()
          .HasMany(x => x.Documents)
          .WithOne(x => x.Customer)
          .HasForeignKey(x => x.CustomerId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Customer>()
          .HasMany(x => x.Nominees)
          .WithOne(x => x.Customer)
          .HasForeignKey(x => x.CustomerId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Customer>()
          .HasMany(x => x.Policies)
          .WithOne(x => x.Customer)
          .HasForeignKey(x => x.CustomerId)
          .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<CustomerAddress>()
          .HasOne(x => x.City)
          .WithMany(x => x.CustomerAddresses)
          .HasForeignKey(x => x.CityId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<CustomerKyc>()
          .HasOne(x => x.DocumentType)
          .WithMany()
          .HasForeignKey(x => x.DocumentTypeId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<CustomerDocument>()
          .HasOne(x => x.DocumentType)
          .WithMany()
          .HasForeignKey(x => x.DocumentTypeId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<CustomerNominee>()
          .HasOne(x => x.Relationship)
          .WithMany()
          .HasForeignKey(x => x.RelationshipId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<InsuranceProduct>()
          .HasOne(x => x.Category)
          .WithMany(x => x.Products)
          .HasForeignKey(x => x.CategoryId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Policy>()
          .HasOne(x => x.Product)
          .WithMany(x => x.Policies)
          .HasForeignKey(x => x.ProductId)
          .OnDelete(DeleteBehavior.Restrict);


      modelBuilder.Entity<Country>()
          .HasMany(x => x.States)
          .WithOne(x => x.Country)
          .HasForeignKey(x => x.CountryId)
          .OnDelete(DeleteBehavior.Restrict);

   

      modelBuilder.Entity<State>()
          .HasMany(x => x.Cities)
          .WithOne(x => x.State)
          .HasForeignKey(x => x.StateId)
          .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<PremiumPayment>()
          .HasOne(p => p.PremiumSchedule)
          .WithMany()
          .HasForeignKey(p => p.ScheduleId)
          .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Claim>()
          .HasOne(c => c.Policy)
          .WithMany()
          .HasForeignKey(c => c.PolicyId)
          .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Claim>()
          .HasOne(c => c.Customer)
          .WithMany()
          .HasForeignKey(c => c.CustomerId)
          .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<Claim>()
          .HasIndex(c => c.ClaimNumber)
          .IsUnique();

      modelBuilder.Entity<UnderwritingCase>()
          .HasOne(u => u.Customer)
          .WithMany()
          .HasForeignKey(u => u.CustomerId)
          .OnDelete(DeleteBehavior.NoAction);

      modelBuilder.Entity<UnderwritingCase>()
          .HasOne(u => u.Product)
          .WithMany()
          .HasForeignKey(u => u.ProductId)
          .OnDelete(DeleteBehavior.NoAction);
    }
  }
}
