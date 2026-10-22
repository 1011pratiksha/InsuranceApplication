using CustomerRegistration.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.Interface
{
    public interface IDocumentService
    {
        Task<string> UploadDocument(int customerId, string documentType, string fileName, byte[] content);
        Task<List<CustomerDocumentDto>> FetchDocuments(int customerId);
        Task<string> DeleteDocument(int documentId);
        Task<string> VerifyDocument(int documentId);
    }
}
