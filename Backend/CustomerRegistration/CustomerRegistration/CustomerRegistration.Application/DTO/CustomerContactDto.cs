using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Application.DTO
{
    public class CreateContactDto
    {
        public string? AlternateMobile { get; set; }

        public string? OfficePhone { get; set; }

        [Required]
        public string PreferredContactMode { get; set; } = "Mobile";
    }

    public class CustomerContactDto : CreateContactDto
    {
        public int ContactId { get; set; }
        public int CustomerId { get; set; }
    }
}
