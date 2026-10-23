using Dashboard.application.DTO;
using Dashboard.application.Interface;
using Dashboard.infrastructure.Config;

namespace Dashboard.infrastructure.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly PolicyClient policyClient;
        private readonly PremiumScheduleClient premiumScheduleClient;
        private readonly ClaimClient claimClient;
        private readonly KycClient kycClient;

        public DashboardRepository(PolicyClient policyClient, PremiumScheduleClient premiumScheduleClient, ClaimClient claimClient, KycClient kycClient)
        {
            this.policyClient = policyClient;
            this.premiumScheduleClient = premiumScheduleClient;
            this.claimClient = claimClient;
            this.kycClient = kycClient;
        }

        public async Task<AdminDashboardDTO> GetAdminDashboardAsync()
        {
            return new AdminDashboardDTO();
        }

        public async Task<AgentDashboardDTO> GetAgentDashboardAsync(int agentId)
        {
            return new AgentDashboardDTO();
        }

        public async Task<CustomerDashboardDTO> GetCustomerDashboardAsync(int customerId)
        {
            var policies = await policyClient.GetAllPoliciesAsync();

            var customerPolicies = policies
                .Where(p => p.CustomerId == customerId)
                .ToList();

            var activePolicies = customerPolicies
                .Where(p => p.PolicyStatus.Equals("Active", StringComparison.OrdinalIgnoreCase))
                .ToList();

            DateTime? nextPremiumDue = null;

            foreach (var policy in activePolicies)
            {
                var schedules = await premiumScheduleClient.GetPremiumScheduleAsync(policy.PolicyId);

                var nextSchedule = schedules
                    .Where(s => s.DueDate.Date >= DateTime.Today &&
                                s.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(s => s.DueDate)
                    .FirstOrDefault();

                if (nextSchedule != null &&
                    (nextPremiumDue == null || nextSchedule.DueDate < nextPremiumDue))
                {
                    nextPremiumDue = nextSchedule.DueDate;
                }
            }

            var claims = await claimClient.GetAllClaimsAsync();

            var openClaims = claims.Count(c =>
                c.CustomerId == customerId &&
                c.ClaimStatus.Equals("Open", StringComparison.OrdinalIgnoreCase));

            var kyc = await kycClient.GetCustomerKycAsync(customerId);

            var dashboard = new CustomerDashboardDTO
            {
                ActivePolicies = activePolicies.Count,
                NextPremiumDue = nextPremiumDue,
                OpenClaims = openClaims,
                KYCStatus = kyc?.VerificationStatus ?? "Not Submitted"
            };

            return dashboard;
        }
    }
}

