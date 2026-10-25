using ClaimManagement.Application.Interface;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimEligibilityRepository : IClaimEligibilityRepository
    {
        private readonly AppDbContext db;

        public ClaimEligibilityRepository(AppDbContext db)
        {
            this.db = db ;
        }

        public async Task<bool> CustomerExistsAsync(long customerId)
        {
            return await db.Customers
                .AnyAsync(c => c.CustomerId == customerId);
        }

        public async Task<bool> PolicyBelongsToCustomerAsync(long policyId, long customerId)
        {
            return await db.Policies
                .AnyAsync(p => p.PolicyId == policyId && p.CustomerId == customerId);
        }

        public async Task<bool> IsPolicyActiveAsync(long policyId, DateTime incidentDate)
        {
            return await db.Policies
                .AnyAsync(p => p.PolicyId == policyId
                    && p.PolicyStatus == "Active"
                    && p.EffectiveDate <= incidentDate
                    && p.ExpiryDate >= incidentDate);
        }

        public async Task<bool> HasValidCoverageAsync(long policyId, DateTime incidentDate, decimal estimatedLoss)
        {
            return await db.PolicyCoverages
                .AnyAsync(c => c.PolicyId == policyId
                    && c.CoverageStartDate <= incidentDate
                    && c.CoverageEndDate >= incidentDate
                    && c.SumInsured >= estimatedLoss);
        }

        public async Task<bool> HasEligiblePremiumAsync(long policyId)
        {
            var schedules = await db.PremiumSchedules
                .Where(s => s.PolicyId == policyId)
                .ToListAsync();

            if (schedules.Count == 0)
            {
                return false;
            }

            foreach (var schedule in schedules.Where(s => s.DueDate <= DateTime.UtcNow))
            {
                var paidAmount = await db.PremiumPayments
                    .Where(p => p.ScheduleId == schedule.ScheduleId
                        && p.PolicyId == policyId
                        && p.PaymentStatus == "Success")
                    .SumAsync(p => (decimal?)p.AmountPaid) ?? 0;

                if (paidAmount < schedule.PremiumAmount + schedule.LateFee)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
