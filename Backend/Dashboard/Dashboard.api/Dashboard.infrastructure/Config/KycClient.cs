using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class KycClient
    {
        private readonly HttpClient client;

        public KycClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<CustomerKycResponseDto?> GetCustomerKycAsync(int customerId)
        {
            try
            {
                var response = await client.GetFromJsonAsync<ApiResponse<CustomerKycResponseDto>>(
                    $"api/v1/kyc/customer/customer/{customerId}");

                return response?.Data;
            }
            catch (HttpRequestException ex) when (
                ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }

    public class CustomerKycResponseDto
    {
        public int KycId { get; set; }
        public int CustomerId { get; set; }
        public string VerificationStatus { get; set; } = string.Empty;
    }
}

