using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class AgentCustomerClient
    {
        private readonly HttpClient client;

        public AgentCustomerClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<AgentCustomerResponseDto>> GetAssignedCustomersAsync(int agentId)
        {
            var customers = await client.GetFromJsonAsync<List<AgentCustomerResponseDto>>(
                $"api/v1/agents/{agentId}/customers");

            return customers ?? new List<AgentCustomerResponseDto>();
        }
    }

    public class AgentCustomerResponseDto
    {
        public int AgentCustomerId { get; set; }
        public int AgentId { get; set; }
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public DateTime AssignedDate { get; set; }
        public int AssignedBy { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

