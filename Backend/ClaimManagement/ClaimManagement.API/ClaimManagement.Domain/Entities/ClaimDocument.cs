using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ClaimManagement.Domain.Entities
{
    public class ClaimDocument
    {
        [Key]
        public int document_id { get; set; }

        [ForeignKey("ClaimMaster")]
        public long claim_id { get; set; }

        [Required]
        [MaxLength(150)]
        public string document_name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? file_path { get; set; }

        [Required]
        [MaxLength(50)]
        public string verification_status { get; set; } = "Pending";

        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        public ClaimMaster? ClaimMaster { get; set; }
    }
}

