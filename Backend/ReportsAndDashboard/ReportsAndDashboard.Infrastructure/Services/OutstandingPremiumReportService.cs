
using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using InsuranceApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class OutstandingPremiumReportService
      : IOutstandingPremiumReportService
  {
    private readonly AppDbContext db;

    public OutstandingPremiumReportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<OutstandingPremiumReportDto>>
        GetOutstandingPremiumReportAsync(
            DateTime? fromDate,
            DateTime? toDate)
    {
      var query =
          from schedule in db.PremiumSchedules.AsNoTracking()
          join policy in db.Policies.AsNoTracking()
              on schedule.PolicyId equals policy.PolicyId
          where schedule.Status == "Pending"
             || schedule.Status == "Overdue"
          select new
          {
            schedule.ScheduleId,
            schedule.PolicyId,
            policy.PolicyNumber,
            schedule.InstallmentNo,
            schedule.DueDate,
            schedule.PremiumAmount,
            schedule.LateFee,
            schedule.Status
          };

      if (fromDate.HasValue)
      {
        query = query.Where(x => x.DueDate >= fromDate.Value);
      }

      if (toDate.HasValue)
      {
        var exclusiveEndDate = toDate.Value.Date.AddDays(1);
        query = query.Where(x => x.DueDate < exclusiveEndDate);
      }

      var schedules = await query.ToListAsync();

      var scheduleIds = schedules
          .Select(x => x.ScheduleId)
          .ToList();

      var payments = await db.PremiumPayments
          .AsNoTracking()
          .Where(p => scheduleIds.Contains(p.ScheduleId)
                   && p.PaymentStatus == "Success")
          .GroupBy(p => p.ScheduleId)
          .Select(g => new
          {
            ScheduleId = g.Key,
            AmountPaid = g.Sum(p => p.AmountPaid)
          })
          .ToDictionaryAsync(x => x.ScheduleId, x => x.AmountPaid);

      return schedules.Select(schedule =>
      {
        payments.TryGetValue(
            schedule.ScheduleId,
            out decimal amountPaid);

        var totalDue = schedule.PremiumAmount + schedule.LateFee;
        var outstanding = Math.Max(0m, totalDue - amountPaid);

        return new OutstandingPremiumReportDto
        {
          ScheduleId = schedule.ScheduleId,
          PolicyId = schedule.PolicyId,
          PolicyNumber = schedule.PolicyNumber,
          InstallmentNo = schedule.InstallmentNo,
          DueDate = schedule.DueDate,
          PremiumAmount = schedule.PremiumAmount,
          LateFee = schedule.LateFee,
          Status = schedule.Status,
          AmountPaid = amountPaid,
          OutstandingAmount = outstanding
        };
      })
      .Where(x => x.OutstandingAmount > 0)
      .OrderBy(x => x.DueDate)
      .ToList();
    }
  }
}
