using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KycVerification.Domain.Entities
{
    public class CustomerKyc
    {
        [Key]
        public int KycId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? KycNumber { get; set; }

        [Required]
        public DateTime KycDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string VerificationStatus { get; set; } = "Pending";

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedDate { get; set; }
    }
}
