namespace ClaimManagement.Application.DTO
{
    public class CustomerDto
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; } = string.Empty;
        public string customer_status { get; set; } = string.Empty;
    }
}
