using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.Controllers
{
    [Route("api/claims/{claimId:int}/documents")]
    [ApiController]
    public class ClaimDocumentsController : ControllerBase
    {
        private readonly IClaimDocumentRepository documentRepository;
        private readonly IClaimRepository claimRepository;
        private readonly IMapper mapper;

        public ClaimDocumentsController(IClaimDocumentRepository documentRepository, IClaimRepository claimRepository, IMapper mapper)
        {
            this.documentRepository = documentRepository;
            this.claimRepository = claimRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument(int claimId, CreateClaimDocumentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (claimId <= 0 || !await claimRepository.ExistsAsync(claimId))
                return NotFound("Claim not found.");

            var document = mapper.Map<ClaimDocument>(dto);
            document.ClaimId = claimId;
            document.VerificationStatus = "Pending";
            document.UploadedDate = DateTime.UtcNow;

            var savedDocument = await documentRepository.AddAsync(document);

            return Ok(mapper.Map<ClaimDocumentResponseDto>(savedDocument));
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments(int claimId)
        {
            if (claimId <= 0 || !await claimRepository.ExistsAsync(claimId))
                return NotFound("Claim not found.");

            var documents = await documentRepository.GetByClaimIdAsync(claimId);

            return Ok(mapper.Map<List<ClaimDocumentResponseDto>>(documents));
        }

        [HttpPut("verify")]
        public async Task<IActionResult> VerifyDocuments(int claimId, [FromBody] List<int> documentIds)
        {
            if (claimId <= 0 || !await claimRepository.ExistsAsync(claimId))
                return NotFound("Claim not found.");

            if (documentIds == null || documentIds.Count == 0)
                return BadRequest("Provide at least one document ID.");

            var documents = await documentRepository.GetByClaimIdAsync(claimId);

            var selectedDocuments = documents
                .Where(d => documentIds.Contains(d.DocumentId))
                .ToList();

            if (selectedDocuments.Count != documentIds.Distinct().Count())
                return BadRequest("One or more document IDs are invalid for this claim.");

            foreach (var document in selectedDocuments)
            {
                document.VerificationStatus = "Verified";
                await documentRepository.UpdateAsync(document);
            }

            return Ok(mapper.Map<List<ClaimDocumentResponseDto>>(selectedDocuments));
        }
    }
}