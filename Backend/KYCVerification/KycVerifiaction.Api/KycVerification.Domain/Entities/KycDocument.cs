using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KycVerification.Domain.Entities
{
    public class KycDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public int KycId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? DocumentType { get; set; }

        [Required]
        [MaxLength(100)]
        public string? DocumentNumber { get; set; } 

        [Required]
        [MaxLength(500)]
        public string? FilePath { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(20)]
        public string VerificationStatus { get; set; } = "Pending";

    }
}
