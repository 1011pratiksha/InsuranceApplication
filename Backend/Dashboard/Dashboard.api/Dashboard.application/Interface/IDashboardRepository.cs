using Dashboard.application.DTO;

namespace Dashboard.application.Interface
{
    public interface IDashboardRepository
    {
        Task<AdminDashboardDTO> GetAdminDashboardAsync();

        Task<AgentDashboardDTO> GetAgentDashboardAsync(int agentId);

        Task<CustomerDashboardDTO> GetCustomerDashboardAsync(int customerId);
    }


}
