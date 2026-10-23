using System.Net.Http.Json;

namespace Dashboard.infrastructure.Config
{
    public class CommissionPaymentClient
    {
        private readonly HttpClient client;

        public CommissionPaymentClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<CommissionPaymentResponseDto>> GetAgentPayoutHistoryAsync(int agentId)
        {
            var payments = await client.GetFromJsonAsync<List<CommissionPaymentResponseDto>>(
                $"api/v1/agents/{agentId}/payments/payouts");

            return payments ?? new List<CommissionPaymentResponseDto>();
        }
    }

    public class CommissionPaymentResponseDto
    {
        public int CommissionPaymentId { get; set; }
        public int AgentId { get; set; }
        public string PaymentReference { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public string? TransactionReference { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

