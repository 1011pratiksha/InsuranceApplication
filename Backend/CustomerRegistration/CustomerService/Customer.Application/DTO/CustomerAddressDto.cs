using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.DTO
{
    public class CustomerAddressDto
    {
        public int AddressId { get; set; }
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
    }
}
