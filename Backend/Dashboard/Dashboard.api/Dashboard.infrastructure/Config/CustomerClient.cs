using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class CustomerClient
    {
        private readonly HttpClient client;

        public CustomerClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers = await client.GetFromJsonAsync<List<CustomerResponseDto>>(
                "api/v1/customers");

            return customers ?? new List<CustomerResponseDto>();
        }
    }

    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CustomerStatus { get; set; } = string.Empty;
    }
}
