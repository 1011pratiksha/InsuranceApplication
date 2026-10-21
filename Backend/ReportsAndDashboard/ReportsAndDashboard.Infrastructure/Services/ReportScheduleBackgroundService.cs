
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using InsuranceApp.Infrastructure.Persistence;
using ReportsAndDashboard.Application.Interfaces;
using ReportsAndDashboard.Domain.Entities.Reporting;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class ReportScheduleBackgroundService
      : BackgroundService, IReportScheduleBackgroundService
  {
    private readonly IServiceScopeFactory scope;
    private readonly ILogger<ReportScheduleBackgroundService> logger;

    public ReportScheduleBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<ReportScheduleBackgroundService> logger)
    {
      scope = scopeFactory;
      this.logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        try
        {
          await ProcessDueSchedulesAsync(stoppingToken);
        }
        catch (OperationCanceledException)
            when (stoppingToken.IsCancellationRequested)
        {
          break;
        }
        catch (Exception ex)
        {
          logger.LogError(
              ex, "Error processing scheduled reports.");
        }

        try
        {
          await Task.Delay(
              TimeSpan.FromMinutes(1), stoppingToken);
        }
        catch (OperationCanceledException)
            when (stoppingToken.IsCancellationRequested)
        {
          break;
        }
      }
    }

    private async Task ProcessDueSchedulesAsync(
        CancellationToken cancellationToken)
    {
      using var scope = this.scope.CreateScope();

      var db = scope.ServiceProvider
          .GetRequiredService<AppDbContext>();

      var exportService = scope.ServiceProvider
          .GetRequiredService<IReportExportService>();

      var now = DateTime.Now;

      var dueSchedules = await db.ReportSchedules
          .Where(s =>
              s.Status == ReportRecordStatus.Active &&
              s.NextRunDate <= now)
          .ToListAsync(cancellationToken);

      foreach (var schedule in dueSchedules)
      {
        cancellationToken.ThrowIfCancellationRequested();

        string? filePath = null;
        var executionStatus = ReportExecutionStatus.Failed;

        try
        {
          var reportExists = await db.Set<ReportMaster>()
              .AnyAsync(
                  r => r.ReportId == schedule.ReportId,
                  cancellationToken);

          if (!reportExists)
          {
            throw new InvalidOperationException(
                $"Report ID {schedule.ReportId} does not exist.");
          }

          // Generate the PDF using the existing export service.
          var pdfBytes = await exportService.ExportPdfAsync();

          var outputDirectory = Path.Combine(
              AppContext.BaseDirectory,
              "GeneratedReports");

          Directory.CreateDirectory(outputDirectory);

          var fileName =
              $"Report_{schedule.ReportId}_Schedule_{schedule.ScheduleId}_{DateTime.UtcNow:yyyyMMddHHmmssfff}.pdf";

          filePath = Path.Combine(outputDirectory, fileName);

          await File.WriteAllBytesAsync(
              filePath, pdfBytes, cancellationToken);

          executionStatus = ReportExecutionStatus.Success;
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
          throw;
        }
        catch (Exception ex)
        {
          logger.LogError(
              ex,
              "Failed to generate PDF for schedule {ScheduleId}.",
              schedule.ScheduleId);
        }

        // Record the result of this scheduled execution.
        db.ReportExecutions.Add(new ReportExecution
        {
          ReportId = schedule.ReportId,
          ExecutedBy = null,
          ExecutionDate = DateTime.UtcNow,
          ExecutionStatus = executionStatus,
          FilePath = filePath
        });

        // Move to the next scheduled occurrence.
        schedule.NextRunDate = GetNextRunDate(
            schedule.NextRunDate,
            schedule.ScheduleType);

        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Schedule {ScheduleId} completed with status {Status}. File: {FilePath}",
            schedule.ScheduleId,
            executionStatus,
            filePath ?? "(not generated)");
      }
    }

    private static DateTime GetNextRunDate(
        DateTime currentRun,
        ReportScheduleType scheduleType)
    {
      return scheduleType switch
      {
        ReportScheduleType.Daily =>
            currentRun.AddDays(1),

        ReportScheduleType.Weekly =>
            currentRun.AddDays(7),

        ReportScheduleType.Monthly =>
            currentRun.AddMonths(1),

        ReportScheduleType.Quarterly =>
            currentRun.AddMonths(3),

        ReportScheduleType.Yearly =>
            currentRun.AddYears(1),

        _ => currentRun.AddDays(1)
      };
    }
  }
}
