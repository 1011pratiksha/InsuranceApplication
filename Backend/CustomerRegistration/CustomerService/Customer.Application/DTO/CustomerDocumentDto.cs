using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.DTO
{
    public class CustomerDocumentDto
    {
        public int DocumentId { get; set; }
        public int CustomerId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
        public string VerificationStatus { get; set; }
    }
}
