
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace KycVerification.Application.DTO
{
    public class UploadKycDocumentDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int KycId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? DocumentType { get; set; }

        [Required]
        [MaxLength(100)]
        public string? DocumentNumber { get; set; }

        [Required]
        public IFormFile? File { get; set; }
    }
}
