using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class UnderwritingAdminClient
    {
        private readonly HttpClient client;

        public UnderwritingAdminClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<UnderwritingAdminResponseDto>> GetAllCasesAsync()
        {
            var cases = await client.GetFromJsonAsync<List<UnderwritingAdminResponseDto>>(
                "api/v1/underwriting");

            return cases ?? new List<UnderwritingAdminResponseDto>();
        }
    }

    public class UnderwritingAdminResponseDto
    {
        public int UnderwritingId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}

