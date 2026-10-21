using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Application.DTO
{
    public class CreateClaimDocumentDto
    {
        [Required]
        [MaxLength(150)]
        public string DocumentName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? FilePath { get; set; }
    }
}