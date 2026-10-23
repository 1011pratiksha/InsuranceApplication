using Dashboard.application.DTO;
using Dashboard.application.Interface;

namespace Dashboard.infrastructure.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        public async Task<AdminDashboardDTO> GetAdminDashboardAsync()
        {
            var dashboard = new AdminDashboardDTO
            {
                TotalCustomers = 500,
                TotalPolicies = 850,
                ActivePolicies = 700,
                PremiumCollected = 2500000,
                PendingPremium = 150000,
                ClaimsRegistered = 120,
                ClaimsSettled = 90,
                OutstandingClaims = 200000,
                MonthlyRevenue = 450000,
                TopPerformingAgent = "Sample Agent"
            };


        return await Task.FromResult(dashboard);
        }

        public async Task<AgentDashboardDTO> GetAgentDashboardAsync(int agentId)
        {
            var dashboard = new AgentDashboardDTO
            {
                TotalCustomers = 45,
                TotalPolicies = 60,
                ActivePolicies = 52,
                PremiumCollected = 350000,
                PendingPremium = 25000,
                ClaimsRegistered = 8,
                TotalCommission = 18000
            };

            return await Task.FromResult(dashboard);
        }

        public async Task<CustomerDashboardDTO> GetCustomerDashboardAsync(int customerId)
        {
            var dashboard = new CustomerDashboardDTO
            {
                TotalPolicies = 3,
                ActivePolicies = 2,
                UpcomingPremium = 5000,
                TotalPremiumPaid = 45000,
                TotalClaims = 2,
                PendingClaims = 1,
                SettledClaims = 1,
                UnreadNotifications = 3
            };

            return await Task.FromResult(dashboard);
        }
    }

}
