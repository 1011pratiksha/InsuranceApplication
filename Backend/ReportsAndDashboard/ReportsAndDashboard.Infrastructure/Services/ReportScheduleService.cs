
using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using ReportsAndDashboard.Domain.Entities.Reporting;
using InsuranceApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class ReportScheduleService : IReportScheduleService
  {
    private readonly AppDbContext db;

    public ReportScheduleService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<ReportScheduleDto>> GetAllSchedulesAsync()
    {
      return await db.ReportSchedules
          .AsNoTracking()
          .Select(x => new ReportScheduleDto
          {
            ScheduleId = x.ScheduleId,
            ReportId = x.ReportId,
            ScheduleType = x.ScheduleType.ToString(),
            NextRunDate = x.NextRunDate,
            EmailTo = x.EmailTo,
            Status = x.Status.ToString()
          })
          .ToListAsync();
    }

    public async Task<ReportScheduleDto?> GetScheduleByIdAsync(long id)
    {
      var schedule = await db.ReportSchedules
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.ScheduleId == id);

      if (schedule == null)
      {
        return null;
      }

      return new ReportScheduleDto
      {
        ScheduleId = schedule.ScheduleId,
        ReportId = schedule.ReportId,
        ScheduleType = schedule.ScheduleType.ToString(),
        NextRunDate = schedule.NextRunDate,
        EmailTo = schedule.EmailTo,
        Status = schedule.Status.ToString()
      };
    }

    public async Task<ReportScheduleDto> CreateScheduleAsync(
        ReportScheduleDto dto)
    {
      if (dto.NextRunDate == null)
      {
        throw new ArgumentException(
            "NextRunDate is required.");
      }

      if (!Enum.TryParse<ReportScheduleType>(
              dto.ScheduleType, true, out var scheduleType) ||
          !Enum.IsDefined(typeof(ReportScheduleType), scheduleType))
      {
        throw new ArgumentException(
            "Invalid ScheduleType.");
      }

      if (!Enum.TryParse<ReportRecordStatus>(
              dto.Status, true, out var status) ||
          !Enum.IsDefined(typeof(ReportRecordStatus), status))
      {
        throw new ArgumentException(
            "Invalid Status.");
      }

      var schedule = new ReportSchedule
      {
        ReportId = dto.ReportId,
        ScheduleType = scheduleType,
        NextRunDate = dto.NextRunDate.Value,
        EmailTo = dto.EmailTo,
        Status = status
      };

      db.ReportSchedules.Add(schedule);
      await db.SaveChangesAsync();

      return new ReportScheduleDto
      {
        ScheduleId = schedule.ScheduleId,
        ReportId = schedule.ReportId,
        ScheduleType = schedule.ScheduleType.ToString(),
        NextRunDate = schedule.NextRunDate,
        EmailTo = schedule.EmailTo,
        Status = schedule.Status.ToString()
      };
    }

    public async Task<bool> UpdateScheduleAsync(
        long id,
        ReportScheduleDto dto)
    {
      if (dto.NextRunDate == null)
      {
        throw new ArgumentException(
            "NextRunDate is required.");
      }

      if (!Enum.TryParse<ReportScheduleType>(
              dto.ScheduleType, true, out var scheduleType) ||
          !Enum.IsDefined(typeof(ReportScheduleType), scheduleType))
      {
        throw new ArgumentException(
            "Invalid ScheduleType.");
      }

      if (!Enum.TryParse<ReportRecordStatus>(
              dto.Status, true, out var status) ||
          !Enum.IsDefined(typeof(ReportRecordStatus), status))
      {
        throw new ArgumentException(
            "Invalid Status.");
      }

      var schedule = await db.ReportSchedules
          .FirstOrDefaultAsync(x => x.ScheduleId == id);

      if (schedule == null)
      {
        return false;
      }

      schedule.ReportId = dto.ReportId;
      schedule.ScheduleType = scheduleType;
      schedule.NextRunDate = dto.NextRunDate.Value;
      schedule.EmailTo = dto.EmailTo;
      schedule.Status = status;

      await db.SaveChangesAsync();

      return true;
    }

    public async Task<bool> DeleteScheduleAsync(long id)
    {
      var schedule = await db.ReportSchedules
          .FirstOrDefaultAsync(x => x.ScheduleId == id);

      if (schedule == null)
      {
        return false;
      }

      db.ReportSchedules.Remove(schedule);
      await db.SaveChangesAsync();

      return true;
    }
  }
}
