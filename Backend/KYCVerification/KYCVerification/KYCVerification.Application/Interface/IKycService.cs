
using KycVerification.Application.DTO;

namespace KycVerification.Application.Interface
{
    public interface IKycService
    {
        // KYC request operations.
        Task<CustomerKycDto> CreateKycAsync(CreateCustomerKycDto dto);
        Task<CustomerKycDto?> GetKycByIdAsync(int kycId);
        Task<List<CustomerKycDto>> GetAllKycAsync();
        Task<bool> ResubmitKycAsync(int kycId);
        Task<bool> ApproveKycAsync(int kycId, ReviewKycDto dto);
        Task<bool> RejectKycAsync(int kycId, ReviewKycDto dto);

        // KYC document operations.
        Task<KycDocumentDto> UploadDocumentAsync(UploadKycDocumentDto dto);
        Task<List<KycDocumentDto>> GetDocumentsAsync(int kycId);
        Task<bool> VerifyDocumentAsync(int documentId, ReviewKycDto dto);
        Task<bool> RejectDocumentAsync(int documentId, ReviewKycDto dto);
        Task<KycDocumentDto> ReuploadDocumentAsync(
            int documentId, UploadKycDocumentDto dto);

        // Dashboard summary.
        Task<KycDashboardDto> GetDashboardAsync();

        Task<CustomerKycDto?> UpdateKycAsync( int kycId, UpdateCustomerKycDto dto);

        Task<bool> DeleteKycAsync(int kycId);
    }
}
