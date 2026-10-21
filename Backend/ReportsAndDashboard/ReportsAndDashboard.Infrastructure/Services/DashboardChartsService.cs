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
  public class DashboardChartsService : IDashboardChartsService
  {
    private readonly AppDbContext db;

    public DashboardChartsService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<DashboardChartDto>> GetDashboardChartsAsync()
    {
      var startDate = new DateTime(DateTime.Today.Year, 1, 1);
      var endDate = startDate.AddYears(1);

      var charts = new List<DashboardChartDto>();

      // 1. Monthly Premium Collection
      var payments = await db.PremiumPayments
          .AsNoTracking()
          .Where(x =>
              x.PaymentStatus == "Success" &&
              x.PaymentDate.HasValue &&
              x.PaymentDate.Value >= startDate &&
              x.PaymentDate.Value < endDate)
          .GroupBy(x => x.PaymentDate!.Value.Month)
          .Select(g => new
          {
            Month = g.Key,
            Total = g.Sum(x => x.AmountPaid)
          })
          .ToListAsync();

      for (int month = 1; month <= 12; month++)
      {
        charts.Add(new DashboardChartDto
        {
          ChartName = "Monthly Premium Collection",
          Label = new DateTime(startDate.Year, month, 1)
                .ToString("MMM"),
          Value = payments
                .Where(x => x.Month == month)
                .Select(x => x.Total)
                .FirstOrDefault()
        });
      }

      // 2. Policy Sales by Month
      var policySales = await db.Policies
          .AsNoTracking()
          .Where(x =>
              x.IssueDate.HasValue &&
              x.IssueDate.Value >= startDate &&
              x.IssueDate.Value < endDate)
          .GroupBy(x => x.IssueDate!.Value.Month)
          .Select(g => new
          {
            Month = g.Key,
            Total = g.Count()
          })
          .ToListAsync();

      for (int month = 1; month <= 12; month++)
      {
        charts.Add(new DashboardChartDto
        {
          ChartName = "Policy Sales",
          Label = new DateTime(startDate.Year, month, 1)
                .ToString("MMM"),
          Value = policySales
                .Where(x => x.Month == month)
                .Select(x => (decimal)x.Total)
                .FirstOrDefault()
        });
      }
     
// 3. Policy by Product
var policiesByProduct = await db.Policies
    .AsNoTracking()
    .GroupBy(x => new
    {
      x.ProductId,
      ProductName = x.Product.ProductName
    })
    .Select(g => new
    {
      ProductName = g.Key.ProductName,
      Total = g.Count()
    })
    .ToListAsync();

      foreach (var product in policiesByProduct)
      {
        charts.Add(new DashboardChartDto
        {
          ChartName = "Policy by Product",
          Label = string.IsNullOrWhiteSpace(product.ProductName)
                ? "Unknown"
                : product.ProductName,
          Value = product.Total
        });
      }

     
// 4. Revenue Trend (based on successful premium collections)
for (int month = 1; month <= 12; month++)
      {
        var monthlyRevenue = payments
            .Where(x => x.Month == month)
            .Select(x => x.Total)
            .FirstOrDefault();

        charts.Add(new DashboardChartDto
        {
          ChartName = "Revenue Trend",
          Label = new DateTime(startDate.Year, month, 1)
                .ToString("MMM"),
          Value = monthlyRevenue
        });
      }


      // 3. Claim Status
      var claimStatuses = await db.Claims
          .AsNoTracking()
          .GroupBy(x => x.ClaimStatus)
          .Select(g => new
          {
            Status = g.Key,
            Total = g.Count()
          })
          .ToListAsync();

      foreach (var claim in claimStatuses)
      {
        charts.Add(new DashboardChartDto
        {
          ChartName = "Claim Status",
          Label = string.IsNullOrWhiteSpace(claim.Status)
                ? "Unknown"
                : claim.Status,
          Value = claim.Total
        });
      }

      return charts;
    }
  }
}

