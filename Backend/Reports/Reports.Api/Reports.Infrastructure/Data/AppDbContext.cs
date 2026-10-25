using Microsoft.EntityFrameworkCore;
using Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<ReportMaster> ReportMasters { get; set; }
        public DbSet<ReportSchedule> ReportSchedules { get; set; }
        public DbSet<ReportExecution> ReportExecutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ReportMaster table
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
                    .HasColumnName("description");

                entity.Property(x => x.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");
            });

            // ReportSchedule table
            modelBuilder.Entity<ReportSchedule>(entity =>
            {
                entity.ToTable("report_schedule");

                entity.HasKey(x => x.ScheduleId);

                entity.Property(x => x.ScheduleId)
                    .HasColumnName("schedule_id")
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.ReportId)
                    .HasColumnName("report_id")
                    .IsRequired();

                entity.Property(x => x.ScheduleType)
                    .HasColumnName("schedule_type")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.NextRunDate)
                    .HasColumnName("next_run_date")
                    .HasColumnType("datetime");

                entity.Property(x => x.EmailTo)
                    .HasColumnName("email_to")
                    .HasMaxLength(250);

                entity.Property(x => x.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasOne(x => x.Report)
                    .WithMany(x => x.Schedules)
                    .HasForeignKey(x => x.ReportId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ReportExecution table
            modelBuilder.Entity<ReportExecution>(entity =>
            {
                entity.ToTable("report_execution");

                entity.HasKey(x => x.ExecutionId);

                entity.Property(x => x.ExecutionId)
                    .HasColumnName("execution_id")
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.ReportId)
                    .HasColumnName("report_id")
                    .IsRequired();

                entity.Property(x => x.ExecutedBy)
                    .HasColumnName("executed_by")
                    .IsRequired();

                entity.Property(x => x.ExecutionDate)
                    .HasColumnName("execution_date")
                    .HasColumnType("datetime");

                entity.Property(x => x.ExecutionStatus)
                    .HasColumnName("execution_status")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.FilePath)
                    .HasColumnName("file_path")
                    .HasMaxLength(500);

                entity.HasOne(x => x.Report)
                    .WithMany(x => x.Executions)
                    .HasForeignKey(x => x.ReportId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

