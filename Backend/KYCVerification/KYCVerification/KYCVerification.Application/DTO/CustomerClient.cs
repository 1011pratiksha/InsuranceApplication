
using KycVerification.Application.DTO;
using System.Net;
using System.Net.Http.Json;

namespace KycVerification.Infrastructure.Config
{
    // Communicates with the Customer microservice over HTTP.
    public class CustomerClient
    {
        private readonly HttpClient _client;

        public CustomerClient(HttpClient client)
        {
            _client = client;
        }

        // Retrieves a customer by ID from the Customer API.
        public async Task<CustomerDto?> FindCustomerById(int id)
        {
            var response = await _client.GetAsync(
                $"/api/v1/customers/{id}");

            // A missing customer is represented by null.
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            // Throw an exception for other unsuccessful responses.
            response.EnsureSuccessStatusCode();

            // Convert the JSON response into CustomerDto.
            return await response.Content.ReadFromJsonAsync<CustomerDto>();
        }
    }
}
