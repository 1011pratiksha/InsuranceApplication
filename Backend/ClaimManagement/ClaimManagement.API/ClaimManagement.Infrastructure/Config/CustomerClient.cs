using System.Net.Http.Json;
using ClaimManagement.Application.DTO;

namespace ClaimManagement.Infrastructure.Config
{
    public class CustomerClient
    {
        HttpClient client;

        public CustomerClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<CustomerDto?> GetCustomerById(int customerId)
        {
            return await client.GetFromJsonAsync<CustomerDto>($"/customers/{customerId}");
        }
    }
}
