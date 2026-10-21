
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
  public class FinancialReportService : IFinancialReportService
  {
    private readonly AppDbContext db;

    public FinancialReportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<FinancialReportDto>> GetPremiumIncomeReportAsync(
        DateTime? fromDate,
        DateTime? toDate)
    {
      var query =
          from payment in db.PremiumPayments.AsNoTracking()
          join policy in db.Policies.AsNoTracking()
              on payment.PolicyId equals policy.PolicyId
          where payment.PaymentStatus == "Success"
          select new FinancialReportDto
          {
            PaymentId = payment.PaymentId,
            PolicyId = policy.PolicyId,
            PolicyNumber = policy.PolicyNumber,
            PaymentDate = payment.PaymentDate,
            AmountPaid = payment.AmountPaid,
            PaymentMode = payment.PaymentMode,
            TransactionReference = payment.TransactionReference,
            PaymentStatus = payment.PaymentStatus
          };

      if (fromDate.HasValue)
      {
        query = query.Where(x => x.PaymentDate >= fromDate.Value);
      }

      if (toDate.HasValue)
      {
        var exclusiveEndDate = toDate.Value.Date.AddDays(1);
        query = query.Where(x => x.PaymentDate < exclusiveEndDate);
      }

      return await query
          .OrderByDescending(x => x.PaymentDate)
          .ToListAsync();
    }

    public async Task<FinancialReportSummaryDto> GetFinancialReportSummaryAsync()
    {
      var totalPremiumIncome = await db.PremiumPayments
          .AsNoTracking()
          .Where(p => p.PaymentStatus == "Success")
          .SumAsync(p => (decimal?)p.AmountPaid) ?? 0m;

      var schedules = await db.PremiumSchedules
          .AsNoTracking()
          .Where(s => s.Status == "Pending" || s.Status == "Overdue")
          .Select(s => new
          {
            s.ScheduleId,
            s.PremiumAmount,
            s.LateFee
          })
          .ToListAsync();

      var scheduleIds = schedules.Select(s => s.ScheduleId).ToList();

      var paidAmounts = await db.PremiumPayments
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

      decimal totalOutstandingPremium = 0m;

      foreach (var schedule in schedules)
      {
        paidAmounts.TryGetValue(schedule.ScheduleId, out decimal amountPaid);

        var due = schedule.PremiumAmount + schedule.LateFee;
        totalOutstandingPremium += Math.Max(0m, due - amountPaid);
      }

      return new FinancialReportSummaryDto
      {
        ReportDate = DateTime.UtcNow,
        TotalPremiumIncome = totalPremiumIncome,
        TotalOutstandingPremium = totalOutstandingPremium
      };
    }
  }
}
