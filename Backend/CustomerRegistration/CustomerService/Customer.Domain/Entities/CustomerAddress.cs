using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomerRegistration.Domain.Entities
{
    public class CustomerAddress
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public string AddressType { get; set; }

        [Required]
        public string AddressLine1 { get; set; } 

        public string? AddressLine2 { get; set; }

        [Required]
        public string City { get; set; } 

        [Required]
        public string District { get; set; } 

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; } 

        [Required]
        public string Pincode { get; set; } 
    }
}
