
using System.Net;
using System.Net.Http.Json;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;

namespace UnderwritingMS.Infrastructure.Clients
{
    public class KycClient : IKycClient
    {
        private readonly HttpClient httpClient;

        public KycClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<kycResponseDTO?> GetKycStatus(int customerId)
        {
            // Provisional route: confirm with the KYC service developer.
            var response = await httpClient.GetAsync(
                $"api/v1/kyc/{customerId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<kycResponseDTO>();
        }
    }
}
