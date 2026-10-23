
using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using InsuranceApp.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class DashboardSummaryService : IDashboardSummaryService
  {
    private readonly AppDbContext db;

    public DashboardSummaryService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
    {
      var today = DateTime.Today;
      var next30Days = today.AddDays(30);

      var totalCustomers = await db.Customers.CountAsync();

      var totalPolicies = await db.Policies.CountAsync();

      var activePolicies = await db.Policies
          .CountAsync(x => x.PolicyStatus == "Active");


      var claimsRegistered = await db.Claims.CountAsync();

      var claimsApproved = await db.Claims
          .CountAsync(x => x.ClaimStatus == "Approved");

      var claimsRejected = await db.Claims
          .CountAsync(x => x.ClaimStatus == "Rejected");

      var claimsSettled = await db.Claims
          .CountAsync(x => x.ClaimStatus == "Settled");

      var renewalDue = await db.Policies
          .CountAsync(x =>
              x.PolicyStatus == "Active" &&
              x.EndDate >= today &&
              x.EndDate <= next30Days);


      var premiumCollected = await db.PremiumPayments
          .Where(x => x.PaymentStatus == "Success")
          .SumAsync(x => (decimal?)x.AmountPaid) ?? 0m;


      var pendingSchedules = await db.PremiumSchedules
          .AsNoTracking()
          .Where(x => x.Status == "Pending" || x.Status == "Overdue")
          .Select(x => new
          {
            x.ScheduleId,
            x.PremiumAmount,
            x.LateFee
          })
          .ToListAsync();

      var pendingScheduleIds = pendingSchedules
          .Select(x => x.ScheduleId)
          .ToList();

      var paymentsBySchedule = await db.PremiumPayments
          .AsNoTracking()
          .Where(x =>
              pendingScheduleIds.Contains(x.ScheduleId) &&
              x.PaymentStatus == "Success")
          .GroupBy(x => x.ScheduleId)
          .Select(g => new
          {
            ScheduleId = g.Key,
            AmountPaid = g.Sum(x => x.AmountPaid)
          })
          .ToDictionaryAsync(x => x.ScheduleId, x => x.AmountPaid);

      decimal pendingPremium = 0m;

      foreach (var schedule in pendingSchedules)
      {
        paymentsBySchedule.TryGetValue(
            schedule.ScheduleId,
            out decimal amountPaid);

        var totalDue = schedule.PremiumAmount + schedule.LateFee;

        pendingPremium += Math.Max(0m, totalDue - amountPaid);
      }



      return new DashboardSummaryDto
      {
        TotalCustomers = totalCustomers,
        TotalPolicies = totalPolicies,
        ActivePolicies = activePolicies,
        PremiumCollected = premiumCollected,
        PendingPremium = pendingPremium,
        ClaimsRegistered = claimsRegistered,
        ClaimsApproved = claimsApproved,
        ClaimsRejected = claimsRejected,
        ClaimsSettled = claimsSettled,
        RenewalDue = renewalDue,
        Revenue = premiumCollected
      };

    }
  }
}
