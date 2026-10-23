using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class PremiumScheduleClient
    {
        private readonly HttpClient client;
    public PremiumScheduleClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<PremiumScheduleDTO>> GetPremiumScheduleAsync(int policyId)
        {
            var response = await client.GetFromJsonAsync<ApiResponse<List<PremiumScheduleDTO>>>(
                $"api/v1/premium-schedules/{policyId}");

            return response?.Data ?? new List<PremiumScheduleDTO>();
        }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
    }

    public class PremiumScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int PolicyId { get; set; }
        public string PremiumFrequency { get; set; } = string.Empty;
        public int InstallmentNo { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal LateFee { get; set; }
        public string Status { get; set; } = string.Empty;
    }


}
