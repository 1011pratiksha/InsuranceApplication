using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class AgentAdminClient
    {
        private readonly HttpClient client;

        public AgentAdminClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<AgentAdminResponseDto>> GetAllAgentsAsync()
        {
            var agents = await client.GetFromJsonAsync<List<AgentAdminResponseDto>>(
                "api/v1/agents");

            return agents ?? new List<AgentAdminResponseDto>();
        }
    }

    public class AgentAdminResponseDto
    {
        public int AgentId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
