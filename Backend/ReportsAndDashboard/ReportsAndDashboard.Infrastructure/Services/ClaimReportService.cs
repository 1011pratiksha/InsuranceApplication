using Microsoft.EntityFrameworkCore;
using InsuranceApp.Infrastructure.Persistence;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class ClaimReportService : IClaimReportService
  {
    private readonly AppDbContext db;

    public ClaimReportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<ClaimReportDto>> GetClaimReportAsync()
    {
      var claims = await db.Claims
          
          .Include(c => c.Policy)
          .Include(c => c.Customer)
          .Select(c => new ClaimReportDto
          {
            ClaimId = c.ClaimId,
            ClaimNumber = c.ClaimNumber,

            PolicyId = c.PolicyId,
            PolicyNumber = c.Policy.PolicyNumber,

            CustomerId = c.CustomerId,
            CustomerName =
                  c.Customer.FirstName + " " +
                  c.Customer.LastName,

            ClaimType = c.ClaimType,
            IncidentDate = c.IncidentDate,
            ClaimDate = c.ClaimDate,
            EstimatedLoss = c.EstimatedLoss,
            ClaimStatus = c.ClaimStatus
          })
          .ToListAsync();

      return claims;
    }
  }
}
