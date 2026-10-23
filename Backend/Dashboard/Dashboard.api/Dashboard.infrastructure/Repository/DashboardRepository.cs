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
        private readonly AgentCustomerClient agentCustomerClient;
        private readonly AgentCommissionClient agentCommissionClient;
        private readonly CommissionPaymentClient commissionPaymentClient;
        private readonly AgentAdminClient agentAdminClient;
        private readonly KycAdminClient kycAdminClient;
        private readonly UnderwritingAdminClient underwritingAdminClient;

        public DashboardRepository(PolicyClient policyClient, PremiumScheduleClient premiumScheduleClient, ClaimClient claimClient, KycClient kycClient, AgentCustomerClient agentCustomerClient, AgentCommissionClient agentCommissionClient,
    CommissionPaymentClient commissionPaymentClient, AgentAdminClient agentAdminClient, KycAdminClient kycAdminClient, UnderwritingAdminClient underwritingAdminClient)
        {
            this.policyClient = policyClient;
            this.premiumScheduleClient = premiumScheduleClient;
            this.claimClient = claimClient;
            this.kycClient = kycClient;
            this.agentCustomerClient = agentCustomerClient;
            this.agentCommissionClient = agentCommissionClient;
            this.commissionPaymentClient = commissionPaymentClient;
            this.agentAdminClient = agentAdminClient;
            this.kycAdminClient = kycAdminClient;
            this.underwritingAdminClient = underwritingAdminClient;
        }

    public async Task<AdminDashboardDTO> GetAdminDashboardAsync()
        {
            var agents = await agentAdminClient.GetAllAgentsAsync();

            var kycRecords = await kycAdminClient.GetAllKycAsync();

            var underwritingCases = await underwritingAdminClient.GetAllCasesAsync();

            var claims = await claimClient.GetAllClaimsAsync();

            var dashboard = new AdminDashboardDTO
            {
                // Count agents awaiting approval
                PendingAgentApprovals = agents.Count(a =>
                    a.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase)),

                // Count KYC records awaiting verification
                KYCQueue = kycRecords.Count(k =>
                    k.VerificationStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase)),

                // Count underwriting cases awaiting review
                OpenUWCases = underwritingCases.Count(c =>
                    c.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase)),

                // Count open claims
                OpenClaims = claims.Count(c =>
                    c.ClaimStatus.Equals("Open", StringComparison.OrdinalIgnoreCase))
            };

            return dashboard;
        }
     
    public async Task<AgentDashboardDTO> GetAgentDashboardAsync(int agentId)
        {
            var customers = await agentCustomerClient.GetAssignedCustomersAsync(agentId);

            var commissions = await agentCommissionClient.GetCommissionsByAgentIdAsync(agentId);

            var payments = await commissionPaymentClient.GetAgentPayoutHistoryAsync(agentId);

            var currentYear = DateTime.Today.Year;

            var dashboard = new AgentDashboardDTO
            {
                // Count customers currently assigned to this agent
                TotalCustomers = customers.Count(c =>
                    c.Status.Equals("Active", StringComparison.OrdinalIgnoreCase)),

                // Count distinct policies associated with this agent's commission records
                PoliciesSold = commissions
                    .Select(c => c.PolicyId)
                    .Distinct()
                    .Count(),

                // Calculate commissions with Pending status
                CommissionPending = commissions
                    .Where(c => c.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                    .Sum(c => c.CommissionAmount),

                // Calculate successful commission payments made during the current year
                CommissionPaidYTD = payments
                    .Where(p => p.Status.Equals("Success", StringComparison.OrdinalIgnoreCase)
                                && p.PaymentDate.HasValue
                                && p.PaymentDate.Value.Year == currentYear)
                    .Sum(p => p.TotalAmount)
            };

            return dashboard;
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

