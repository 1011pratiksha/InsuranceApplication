using Microsoft.EntityFrameworkCore;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using InsuranceApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Infrastructure.Services
{
  public class PremiumReportService : IPremiumReportService
  {
    private readonly AppDbContext db;

    public PremiumReportService(AppDbContext db
      )
    {
      this.db = db;
    }

    public async Task<List<PremiumReportDto>> GetPremiumReportAsync()
    {
      var data = await (
          from schedule in db.PremiumSchedules.AsNoTracking()
          join policy in db.Policies.AsNoTracking()
              on schedule.PolicyId equals policy.PolicyId
          join customer in db.Customers.AsNoTracking()
              on policy.CustomerId equals customer.CustomerId
          join payment in db.PremiumPayments.AsNoTracking()
              on schedule.ScheduleId equals payment.ScheduleId into payments
          from payment in payments.DefaultIfEmpty()
          select new PremiumReportDto
          {
            ScheduleId = schedule.ScheduleId,
            PolicyId = policy.PolicyId,
            PolicyNumber = policy.PolicyNumber,
            CustomerId = customer.CustomerId,
            CustomerName = customer.FirstName + " " + customer.LastName,
            InstallmentNo = schedule.InstallmentNo,
            DueDate = schedule.DueDate,
            PremiumAmount = schedule.PremiumAmount,
            LateFee = schedule.LateFee,
            ScheduleStatus = schedule.Status,

            PaymentId = payment == null ? (long?)null : payment.PaymentId,
            PaymentDate = payment == null ? null : payment.PaymentDate,
            AmountPaid = payment == null ? (decimal?)null : payment.AmountPaid,
            PaymentMode = payment == null ? null : payment.PaymentMode,
            TransactionReference = payment == null
                  ? null
                  : payment.TransactionReference,
            PaymentStatus = payment == null ? null : payment.PaymentStatus
          }
      ).ToListAsync();

      return data;
    }
  }
}
