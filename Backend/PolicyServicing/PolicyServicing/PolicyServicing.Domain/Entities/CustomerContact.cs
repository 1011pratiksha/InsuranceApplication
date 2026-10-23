using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
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
