
namespace KycVerification.Application.DTO
{
    // Holds the customer details returned by the Customer API.
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string? CustomerCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public string? CustomerStatus { get; set; }
    }
}
