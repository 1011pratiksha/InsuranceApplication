using CustomerRegistration.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerRegistration.Application.Interfaces
{
    public interface ICustomerDocumentService
    {
        Task<string> UploadDocument(int customerId, UploadDocumentDto dto);
        Task<List<DocumentResponseDto>> FetchDocumentsByCustomerId(int customerId);
        Task<DocumentResponseDto?> FindDocumentById(int id);
        Task<string> VerifyDocument(int id, string status);
        Task<string> DeleteDocument(int id);
    }
}
