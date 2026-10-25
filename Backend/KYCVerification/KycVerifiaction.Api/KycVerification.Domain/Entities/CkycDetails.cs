using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KycVerification.Domain.Entities
{
    public class CkycDetails
    {
        [Key]
        public int CkycId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [MaxLength(30)]
        public string? CkycNumber { get; set; }

        public DateTime? VerificationDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string CkycStatus { get; set; } = "Pending";
    }
}
