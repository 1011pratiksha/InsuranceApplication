namespace ClaimManagement.Application.DTO
{
    public class SettlementResponseDTO
    {
        public long SettlementId { get; set; }
        public long ClaimId { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string PaymentMode { get; set; } = "";
        public string? TransactionReference { get; set; }
        public DateTime? SettlementDate { get; set; }
        public string SettlementStatus { get; set; } = "";
    }
}