using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;


namespace CustomerRegistration.Application.DTO
{
    public class UploadDocumentRequest
    {
        public string DocumentType { get; set; } 
        public IFormFile File { get; set; }
    }
}
