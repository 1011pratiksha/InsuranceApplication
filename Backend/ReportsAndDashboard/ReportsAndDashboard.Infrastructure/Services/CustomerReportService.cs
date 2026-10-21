
using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using InsuranceApp.Infrastructure.Persistence;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class CustomerReportService : ICustomerReportService
  {
    private readonly AppDbContext db;

    public CustomerReportService(AppDbContext db)
    {
      this.db = db;
    }

    public async Task<List<CustomerReportDto>> GetCustomerReportAsync()
    {
      return await db.Customers
          .Select(c => new CustomerReportDto
          {
            CustomerId = c.CustomerId,
            CustomerCode = c.CustomerCode,

            FirstName = c.FirstName,
            MiddleName = c.MiddleName,
            LastName = c.LastName,
            DateOfBirth = c.DateOfBirth,

            GenderName = c.Gender.GenderName,
            MaritalStatusName = c.MaritalStatus.MaritalStatusName,
            OccupationName = c.Occupation.OccupationName,

            MobileNumber = c.MobileNumber,
            Email = c.Email,
            AnnualIncome = c.AnnualIncome,
            CustomerStatus = c.CustomerStatus,
            CreatedAt = c.CreatedAt,

            CityName = c.Addresses
                  .Where(a => a.IsCurrent)
                  .Select(a => a.City.CityName)
                  .FirstOrDefault(),

            StateName = c.Addresses
                  .Where(a => a.IsCurrent)
                  .Select(a => a.City.State.StateName)
                  .FirstOrDefault(),

            KycStatus = c.KycRecords
                  .OrderByDescending(k => k.SubmittedAt)
                  .Select(k => k.VerificationStatus)
                  .FirstOrDefault()
          })
          .ToListAsync();
    }
  }
}
