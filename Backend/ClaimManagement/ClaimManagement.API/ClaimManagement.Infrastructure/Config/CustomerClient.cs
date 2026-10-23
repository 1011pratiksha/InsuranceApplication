using System.Net.Http.Json;

namespace ClaimManagement.Infrastructure.Config
{
    public class CustomerDto
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; } = string.Empty;
        public string customer_status { get; set; } = string.Empty;
    }

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
