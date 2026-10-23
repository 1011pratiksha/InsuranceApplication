
using KycVerification.Application.DTO;

namespace KycVerification.Application.Interface
{
    // Defines the operations provided by the KYC service.
    public interface IKycService
    {
        Task<CustomerKycDto> CreateKycAsync(CreateCustomerKycDto dto);

        Task<CustomerKycDto?> GetKycByIdAsync(int kycId);

        Task<List<CustomerKycDto>> GetAllKycAsync();

        Task<KycDocumentDto> UploadDocumentAsync(
            UploadKycDocumentDto dto);

        Task<bool> ApproveKycAsync(int kycId, ReviewKycDto dto);

        Task<bool> RejectKycAsync(int kycId, ReviewKycDto dto);

        Task<List<KycDocumentDto>> GetDocumentsAsync(int kycId);

        Task<bool> VerifyDocumentAsync(
            int documentId, ReviewKycDto dto);

        Task<bool> RejectDocumentAsync(
            int documentId, ReviewKycDto dto);

        Task<KycDocumentDto> ReuploadDocumentAsync(
            int documentId, UploadKycDocumentDto dto);
    }
}
