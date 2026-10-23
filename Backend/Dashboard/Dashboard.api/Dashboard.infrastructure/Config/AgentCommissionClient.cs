using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class AgentCommissionClient
    {
        private readonly HttpClient client;

        public AgentCommissionClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<AgentCommissionResponseDto>> GetCommissionsByAgentIdAsync(int agentId)
        {
            var commissions = await client.GetFromJsonAsync<List<AgentCommissionResponseDto>>(
                $"api/v1/agents/{agentId}/commissions");

            return commissions ?? new List<AgentCommissionResponseDto>();
        }
    }

    public class AgentCommissionResponseDto
    {
        public int CommissionId { get; set; }
        public int AgentId { get; set; }
        public int PolicyId { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal CommissionAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime EarnedDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

