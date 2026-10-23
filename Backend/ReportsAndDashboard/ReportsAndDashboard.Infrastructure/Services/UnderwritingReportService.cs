
using Microsoft.EntityFrameworkCore;
using InsuranceApp.Infrastructure.Persistence;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class UnderwritingReportService : IUnderwritingReportService
  {
    private readonly AppDbContext db;

    public UnderwritingReportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<UnderwritingReportDto>> GetUnderwritingReportAsync()
    {
      var cases = await db.UnderwritingCases
          .AsNoTracking()
          .Include(u => u.Customer)
          .Include(u => u.Product)
          .Select(u => new UnderwritingReportDto
          {
            UnderwritingId = u.UnderwritingId,
            QuoteId = u.QuoteId,
            CustomerId = u.CustomerId,
            CustomerName = u.Customer.FirstName + " " + u.Customer.LastName,
            ProductId = u.ProductId,
            ProductName = u.Product.ProductName,
            UnderwritingDate = u.UnderwritingDate,
            Status = u.Status
          })
          .ToListAsync();

      return cases;
    }
  }
}
