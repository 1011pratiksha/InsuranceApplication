using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimDocumentRepository : IClaimDocumentRepository
    {
        AppDbContext context;
        IMapper mapper;

        public ClaimDocumentRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ClaimDocumentResponseDto> AddDocumentAsync(int claimId, CreateClaimDocumentDto dto)
        {
            var document = mapper.Map<ClaimDocument>(dto);
            document.claim_id = claimId;
            document.verification_status = "Pending";
            document.UploadedDate = DateTime.UtcNow;

            await context.ClaimDocuments.AddAsync(document);
            await context.SaveChangesAsync();

            return mapper.Map<ClaimDocumentResponseDto>(document);
        }

        public async Task<List<ClaimDocumentResponseDto>> GetDocumentsByClaimIdAsync(int claimId)
        {
            var data = await context.ClaimDocuments
                .Where(d => d.claim_id == claimId)
                .OrderByDescending(d => d.UploadedDate)
                .ToListAsync();

            return mapper.Map<List<ClaimDocumentResponseDto>>(data);
        }

        // Returns null if the list is empty or any ID does not belong to this claim
        public async Task<List<ClaimDocumentResponseDto>?> VerifyDocumentsAsync(int claimId, List<int> documentIds)
        {
            if (documentIds == null || documentIds.Count == 0)
                return null;

            var ids = documentIds.Distinct().ToList();

            var documents = await context.ClaimDocuments
                .Where(d => d.claim_id == claimId && ids.Contains(d.document_id))
                .ToListAsync();

            if (documents.Count != ids.Count)
                return null;

            foreach (var document in documents)
            {
                document.verification_status = "Verified";
            }
            await context.SaveChangesAsync();

            return mapper.Map<List<ClaimDocumentResponseDto>>(documents);
        }
    }
}