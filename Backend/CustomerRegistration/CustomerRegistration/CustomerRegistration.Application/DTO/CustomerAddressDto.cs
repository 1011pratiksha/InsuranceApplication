using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Application.DTO
{
    public class CreateAddressDto
    {
        [Required]
        public string AddressType { get; set; } = "Permanent";

        [Required]
        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string District { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string Pincode { get; set; } = string.Empty;
    }

    public class CustomerAddressDto : CreateAddressDto
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
    }
}
