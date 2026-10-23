using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.DTO
{
    public class UpdateCustomerDto
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public decimal AnnualIncome { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
