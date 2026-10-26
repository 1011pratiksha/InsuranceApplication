
using System.Net;
using System.Net.Http.Json;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;

namespace UnderwritingMS.Infrastructure.Clients
{
    public class CustomerRegistrationClient
        : ICustomerRegistrationClient
    {
        private readonly HttpClient _httpClient;

        public CustomerRegistrationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<customerRegistrationResponseDTO?>
            GetCustomerById(int customerId)
        {
            var response = await _httpClient.GetAsync(
                $"api/v1/customers/{customerId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<customerRegistrationResponseDTO>();
        }
    }
}
