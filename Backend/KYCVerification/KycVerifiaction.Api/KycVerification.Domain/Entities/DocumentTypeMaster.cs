using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KycVerification.Domain.Entities
{
    public class DocumentTypeMaster
    {
        [Key]
        public int DocumentTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? DocumentName { get; set; } 

        public bool MandatoryFlag { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
    }
}
