
namespace KycVerification.Application.DTO
{
    // Defines supported document types and whether they are mandatory.
    public class DocumentTypeMasterDto
    {
        public int DocumentTypeId { get; set; }
        public string? DocumentName { get; set; }
        public bool MandatoryFlag { get; set; }
        public string? Status { get; set; }
    }
}
