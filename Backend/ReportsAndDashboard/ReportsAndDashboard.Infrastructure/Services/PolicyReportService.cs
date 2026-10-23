using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using InsuranceApp.Infrastructure.Persistence;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class PolicyReportService : IPolicyReportService
  {
    private readonly AppDbContext db;

    public PolicyReportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<PolicyReportDto>> GetPolicyReportAsync()
    {
      var data = await db.Policies
         
          .Select(p => new PolicyReportDto
          {
            PolicyId = p.PolicyId,
            PolicyNumber = p.PolicyNumber,

            CustomerId = p.CustomerId,
            CustomerName = p.Customer.FirstName + " " + p.Customer.LastName,

            ProductId = p.ProductId,
            ProductName = p.Product.ProductName,

            PolicyStatus = p.PolicyStatus,
            IssueDate = p.IssueDate,
            StartDate = p.StartDate,
            EndDate = p.EndDate
          })
          .ToListAsync();

      return data;
    }
  }
}
