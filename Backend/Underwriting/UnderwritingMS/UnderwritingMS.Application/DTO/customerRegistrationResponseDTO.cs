
namespace UnderwritingMS.Application.DTO
{
    public class customerRegistrationResponseDTO
    {
        public int CustomerId { get; set; }
        public string CustomerStatus { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Occupation { get; set; } = string.Empty;
        public decimal AnnualIncome { get; set; }
    }
}
