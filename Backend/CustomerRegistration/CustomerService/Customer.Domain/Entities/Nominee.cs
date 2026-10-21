using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomerRegistration.Domain.Entities
{
    public class Nominee
    {
        [Key]
        public int NomineeId { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Required]
        public string NomineeName { get; set; } 

        [Required]
        public string Relationship { get; set; } 

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public string? MobileNumber { get; set; }

        [Required]
        public decimal SharePercentage { get; set; }
    }
}
