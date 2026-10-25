using System;
using System.Collections.Generic;

namespace CustomerRegistration.Application.DTO
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string CustomerStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public decimal AnnualIncome { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CustomerStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<CustomerAddressDto> Addresses { get; set; } = new();
        public List<CustomerContactDto> Contacts { get; set; } = new();
        public List<NomineeDto> Nominees { get; set; } = new();
        public List<CustomerDocumentDto> Documents { get; set; } = new();
    }
}
