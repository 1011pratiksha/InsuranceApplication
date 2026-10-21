using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolicyRenewal.Application.DTO;
using PolicyRenewal.Application.Interface;
using PolicyRenewal.Domain.Entities;
using PolicyRenewal.Infrastructure.Data;

namespace PolicyRenewal.Infrastructure.Services
{
    public class PolicyRenewalService : IPolicyRenewalService
    {
        AppDbContext db;
        IMapper mapper;
        public PolicyRenewalService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<PolicyRenewalResponseDto> RenewPolicy(int policyId, RenewPolicyRequestDto request)
        {
            DateTime today = DateTime.Today;
            DateTime expiryDate = request.CurrentExpiryDate.Date;

            if (request.RenewalTermInYears <= 0)
                throw new InvalidOperationException("Renewal term must be at least 1 year.");

            if (request.PremiumAmount <= 0)
                throw new InvalidOperationException("Premium amount must be greater than zero.");

            if (expiryDate < today)
                throw new InvalidOperationException("Policy has already expired and cannot be renewed.");

            if (expiryDate > today.AddDays(30))
                throw new InvalidOperationException("Policy can be renewed only within 30 days of its expiry date.");

            if (await db.PolicyRenewals.AnyAsync(r => r.policy_id == policyId
                                                   && r.renewal_date == expiryDate
                                                   && r.renewal_status == "Renewed"))
                throw new InvalidOperationException("Policy is already renewed for this expiry date.");

            var renewal = await db.PolicyRenewals.FirstOrDefaultAsync(r => r.policy_id == policyId
                                                                        && r.renewal_date == expiryDate
                                                                        && r.renewal_status == "Pending");

            if (renewal == null)
            {
                renewal = new Policy_Renewal
                {
                    policy_id = policyId,
                    renewal_date = expiryDate
                };
                db.PolicyRenewals.Add(renewal);
            }

            renewal.new_expiry_date = expiryDate.AddYears(request.RenewalTermInYears);
            renewal.premium_amount = request.PremiumAmount;

            renewal.renewal_status = request.IsPremiumPaid ? "Renewed" : "Pending";

            await db.SaveChangesAsync();

            return mapper.Map<PolicyRenewalResponseDto>(renewal);
        }

        public async Task<PolicyRenewalResponseDto> GetRenewal(int renewalId)
        {
            var renewal = await db.PolicyRenewals.FindAsync(renewalId);

            if (renewal == null)
                return null;

            return mapper.Map<PolicyRenewalResponseDto>(renewal);
        }

        public async Task ExpirePendingRenewals()
        {
            DateTime today = DateTime.Today;

            var renewals = await db.PolicyRenewals
                .Where(r => r.renewal_status == "Pending" && r.renewal_date < today)
                .ToListAsync();

            foreach (var item in renewals)
            {
                item.renewal_status = "Expired";
            }

            await db.SaveChangesAsync();
        }

        public async Task SendRenewalReminder()
        {
            DateTime today = DateTime.Today;
            int[] reminderDays = { 15, 7, 1 };
            var reminderDates = reminderDays.Select(d => today.AddDays(d)).ToList();

            var renewals = await db.PolicyRenewals
                .Where(r => r.renewal_status == "Pending" && reminderDates.Contains(r.renewal_date))
                .ToListAsync();

        }
    }
}