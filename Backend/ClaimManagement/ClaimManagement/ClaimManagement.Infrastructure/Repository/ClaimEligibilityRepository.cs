using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimEligibilityRepository : IClaimEligibilityRepository
    {
        AppDbContext context;

        public ClaimEligibilityRepository(AppDbContext context)
        {
            this.context = context;
        }

        // Returns "" if eligible, otherwise the error message
        public async Task<string> CheckEligibilityAsync(CreateClaimDto dto)
        {
            // 1. Incident date
            if (dto.IncidentDate == DateTime.MinValue || dto.IncidentDate.Date > DateTime.UtcNow.Date)
                return "Invalid incident date.";

            // 2. Customer
            var customer = await context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == dto.CustomerId);
            if (customer == null)
                return "Customer does not exist.";

            // 3. Policy belongs to customer
            var policy = await context.Policies
                .FirstOrDefaultAsync(p => p.PolicyId == dto.PolicyId && p.CustomerId == dto.CustomerId);
            if (policy == null)
                return "Policy does not belong to this customer.";

            // 4. Policy is active on the incident date
            if (policy.PolicyStatus != "Active"
                || policy.EffectiveDate > dto.IncidentDate
                || policy.ExpiryDate < dto.IncidentDate)
                return "Policy is not active or does not cover the incident date.";

            // 5. Coverage
            var coverage = await context.PolicyCoverages
                .FirstOrDefaultAsync(c => c.PolicyId == dto.PolicyId
                    && c.CoverageStartDate <= dto.IncidentDate
                    && c.CoverageEndDate >= dto.IncidentDate
                    && c.SumInsured >= dto.EstimatedLoss);
            if (coverage == null)
                return "Valid coverage was not found.";

            // 6. Premium: every schedule that is already due must be fully paid
            var schedules = await context.PremiumSchedules
                .Where(s => s.PolicyId == dto.PolicyId)
                .ToListAsync();
            if (schedules.Count == 0)
                return "Premium eligibility requirements are not satisfied.";

            foreach (var schedule in schedules)
            {
                if (schedule.DueDate > DateTime.UtcNow)
                    continue;

                var payments = await context.PremiumPayments
                    .Where(p => p.ScheduleId == schedule.ScheduleId
                        && p.PolicyId == dto.PolicyId
                        && p.PaymentStatus == "Success")
                    .ToListAsync();

                decimal paidAmount = 0;
                foreach (var payment in payments)
                {
                    paidAmount += payment.AmountPaid;
                }

                if (paidAmount < schedule.PremiumAmount + schedule.LateFee)
                    return "Premium eligibility requirements are not satisfied.";
            }

            return "";
        }
    }
}