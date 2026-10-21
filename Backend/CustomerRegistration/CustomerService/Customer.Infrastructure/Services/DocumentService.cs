using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistration.Infrastructure.Services
{
    public class DocumentService : IDocumentService
    {
        AppDbContext db;
        IMapper mapper;
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "Documents");
        public DocumentService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<string> UploadDocument(int customerId, string documentType, string fileName, byte[] content)
        {
            var customer = await db.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return "Customer not found";
            }

            string extension = Path.GetExtension(fileName).ToLower();

            if (!new[] { ".pdf", ".jpg", ".jpeg", ".png" }.Contains(extension))
            {
                return "Only PDF, JPG, JPEG and PNG files are allowed";
            }

            if (content.Length == 0 || content.Length > 5242880)
            {
                return "Select a file up to 5 MB";
            }

            Directory.CreateDirectory(folder);

            string storedName = Guid.NewGuid() + extension;
            string path = Path.Combine(folder, storedName);

            var document = new CustomerDocument
            {
                CustomerId = customerId,
                DocumentType = documentType,
                FileName = Path.GetFileName(fileName),
                FilePath = storedName,
                UploadedDate = DateTime.UtcNow,
                VerificationStatus = "Pending"
            };

            await File.WriteAllBytesAsync(path, content);

            try
            {
                db.CustomerDocuments.Add(document);
                await db.SaveChangesAsync();
            }
            catch
            {
                File.Delete(path);
                throw;
            }

            return "Document uploaded successfully";
        }

        public async Task<List<CustomerDocumentDto>> FetchDocuments(int customerId)
        {
            var data = await db.CustomerDocuments.Where(x => x.CustomerId == customerId).ToListAsync();
            return mapper.Map<List<CustomerDocumentDto>>(data);
        }

        public async Task<string> DeleteDocument(int documentId)
        {
            var document = await db.CustomerDocuments.FindAsync(documentId);
            if (document == null)
            {
                return "Document not found";
            }

            string path = Path.Combine(folder, Path.GetFileName(document.FilePath));
            db.CustomerDocuments.Remove(document);
            await db.SaveChangesAsync();

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return "Document deleted successfully";
        }
        public async Task<string> VerifyDocument(int documentId)
        {
            var document = await db.CustomerDocuments.FindAsync(documentId);
            if (document == null)
            {
                return "Document not found";
            }

            document.VerificationStatus = "Verified";
            await db.SaveChangesAsync();
            return "Document verified successfully";
        }
    }
}