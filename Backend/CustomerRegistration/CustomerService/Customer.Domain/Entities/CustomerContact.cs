using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomerRegistration.Domain.Entities
{
    public class CustomerContact
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        
        public string? AlternateMobile { get; set; }

        public string? OfficePhone { get; set; }

        [Required]
        public string PreferredContactMode { get; set; }
    }
}
