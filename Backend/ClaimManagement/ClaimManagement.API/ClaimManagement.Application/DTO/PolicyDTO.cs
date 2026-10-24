namespace ClaimManagement.Application.DTO
{
    public class PolicyDto
    {
        public int policy_id { get; set; }
        public int customer_id { get; set; }
        public int product_id { get; set; }
        public string policy_status { get; set; } = string.Empty;
        public DateTime effective_date { get; set; }
        public DateTime expiry_date { get; set; }
        public decimal sum_insured { get; set; }
    }
}
