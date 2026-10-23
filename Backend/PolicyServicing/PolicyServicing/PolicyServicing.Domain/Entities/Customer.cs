using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public decimal AnnualIncome { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CustomerStatus { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CustomerAddress>? CustomerAddresses { get; set; }
        public List<CustomerContact>? CustomerContacts { get; set; }
        public List<CustomerDocument>? CustomerDocuments { get; set; }
        public List<Nominee>? Nominees { get; set; }

    }
}
