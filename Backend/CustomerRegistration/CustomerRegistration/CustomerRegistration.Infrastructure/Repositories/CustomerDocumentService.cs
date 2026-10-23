using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interfaces;
using CustomerRegistration.Domain.Entities;
using CustomerRegistration.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistration.Infrastructure.Repositories
{
    public class CustomerDocumentService : ICustomerDocumentService
    {
        AppDbContext db;
        IMapper mapper;

        public CustomerDocumentService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<string> UploadDocument(int customerId, UploadDocumentDto dto)
        {
            var customer = await db.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return "Customer not found";
            }

            var document = mapper.Map<CustomerDocument>(dto);
            document.CustomerId = customerId;
            document.UploadDate = DateTime.UtcNow;
            document.VerificationStatus = "Pending";

            db.CustomerDocuments.Add(document);
            await db.SaveChangesAsync();
            return "Document uploaded successfully";
        }

        public async Task<List<DocumentResponseDto>> FetchDocumentsByCustomerId(int customerId)
        {
            var data = await db.CustomerDocuments
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.UploadDate)
                .ToListAsync();

            return mapper.Map<List<DocumentResponseDto>>(data);
        }

        public async Task<DocumentResponseDto?> FindDocumentById(int id)
        {
            var document = await db.CustomerDocuments.FindAsync(id);
            if (document == null)
            {
                return null;
            }
            return mapper.Map<DocumentResponseDto>(document);
        }

        public async Task<string> VerifyDocument(int id, string status)
        {
            var document = await db.CustomerDocuments.FindAsync(id);
            if (document == null)
            {
                return "Document not found";
            }

            document.VerificationStatus = status;
            await db.SaveChangesAsync();
            return $"Document marked as {status}";
        }

        public async Task<string> DeleteDocument(int id)
        {
            var document = await db.CustomerDocuments.FindAsync(id);
            if (document == null)
            {
                return "Document not found";
            }

            db.CustomerDocuments.Remove(document);
            await db.SaveChangesAsync();
            return "Document deleted successfully";
        }
    }
}
