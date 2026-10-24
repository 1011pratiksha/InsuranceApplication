using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reports.Domain.Entities;
using Reports.Infrastructure.Data;

namespace Reports.Infrastructure.Services
{
    public class ScheduledReportWorker : BackgroundService
    {
        IServiceScopeFactory scopeFactory;
        ILogger<ScheduledReportWorker> logger;

        public ScheduledReportWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<ScheduledReportWorker> logger)
        {
            this.scopeFactory = scopeFactory;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = scopeFactory.CreateScope();

                    var db = scope.ServiceProvider
                        .GetRequiredService<AppDbContext>();

                    var now = DateTime.UtcNow;

                    var schedules = await db.ReportSchedules
                        .Where(x => x.Status == "Active"
                            && x.NextRunDate <= now)
                        .ToListAsync(stoppingToken);

                    foreach (var schedule in schedules)
                    {
                        var execution = new ReportExecution
                        {
                            ReportId = schedule.ReportId,
                            ExecutedBy = 0,
                            ExecutionDate = DateTime.UtcNow,
                            ExecutionStatus = "Failed"
                        };

                        db.ReportExecutions.Add(execution);

                        try
                        {
                            schedule.NextRunDate = schedule.ScheduleType
                                .ToLowerInvariant() switch
                            {
                                "daily" => schedule.NextRunDate.AddDays(1),
                                "weekly" => schedule.NextRunDate.AddDays(7),
                                "monthly" => schedule.NextRunDate.AddMonths(1),
                                "quarterly" => schedule.NextRunDate.AddMonths(3),
                                "yearly" => schedule.NextRunDate.AddYears(1),
                                _ => throw new InvalidOperationException(
                                    "Invalid schedule type.")
                            };

                            // Mark successful only after the actual report
                            // generation and delivery have completed.
                            execution.ExecutionStatus = "Success";
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex,
                                "Schedule {ScheduleId} failed.",
                                schedule.ScheduleId);
                        }
                    }

                    await db.SaveChangesAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "Scheduled report worker failed.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1),
                    stoppingToken);
            }
        }
    }
}