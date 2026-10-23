
using System.ComponentModel.DataAnnotations;

namespace KycVerification.Domain.Entities
{
    public class DocumentTypeMaster
    {
        [Key]
        public int DocumentTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DocumentName { get; set; } = string.Empty;

        public bool MandatoryFlag { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
    }
}
