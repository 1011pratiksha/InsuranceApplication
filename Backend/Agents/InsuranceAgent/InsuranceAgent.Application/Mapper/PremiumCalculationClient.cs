
using InsuranceAgent.Application.DTO;
using System.Net.Http.Json;

namespace InsuranceAgent.Application.Mapper
{
    public class PremiumCalculationClient
    {
        private readonly HttpClient client;

        public PremiumCalculationClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PremiumCalculationDTO?> GetPremiumByQuoteId(int quoteId)
        {
            return await client.GetFromJsonAsync<PremiumCalculationDTO>(
                $"api/v1/quotes/{quoteId}/premium");
        }
    }
}
