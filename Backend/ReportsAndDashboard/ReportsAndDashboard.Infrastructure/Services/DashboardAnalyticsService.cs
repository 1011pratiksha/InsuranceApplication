
using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using ReportsAndDashboard.Domain.Entities;
using InsuranceApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class DashboardAnalyticsService : IDashboardAnalyticsService
  {
    private readonly AppDbContext db;

    public DashboardAnalyticsService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<DashboardAnalyticsDto>> GetDashboardAnalyticsAsync()
    {
      return await db.AnalyticsSnapshots
          
          .OrderByDescending(x => x.SnapshotDate)
          .Select(x => new DashboardAnalyticsDto
          {
            SnapshotId = x.SnapshotId,
            SnapshotDate = x.SnapshotDate,
            TotalCustomers = x.TotalCustomers,
            TotalPolicies = x.TotalPolicies,
            PremiumCollected = x.PremiumCollected,
            ClaimsPaid = x.ClaimsPaid,
            Revenue = x.Revenue
          })
          .ToListAsync();
    }
  }
}
