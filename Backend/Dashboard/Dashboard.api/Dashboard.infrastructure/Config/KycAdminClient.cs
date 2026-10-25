using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class KycAdminClient
    {
        private readonly HttpClient client;

        public KycAdminClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<KycAdminResponseDto>> GetAllKycAsync()
        {
            var records = await client.GetFromJsonAsync<List<KycAdminResponseDto>>(
                "api/v1/kyc");

            return records ?? new List<KycAdminResponseDto>();
        }
    }

    public class KycAdminResponseDto
    {
        public int KycId { get; set; }
        public string VerificationStatus { get; set; } = string.Empty;
    }
}

