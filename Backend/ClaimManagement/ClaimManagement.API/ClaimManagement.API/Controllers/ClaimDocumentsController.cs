using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClaimDocumentsController : ControllerBase
    {
        IClaimDocumentRepository documentRepository;
        IClaimRepository claimRepository;

        public ClaimDocumentsController(IClaimDocumentRepository documentRepository, IClaimRepository claimRepository)
        {
            this.documentRepository = documentRepository;
            this.claimRepository = claimRepository;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("customer/claims/{claimId:int}/documents")]
        public async Task<IActionResult> AddDocument(int claimId, CreateClaimDocumentDto dto)
        {
            var claim = await claimRepository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var document = await documentRepository.AddDocumentAsync(claimId, dto);
            return Ok(document);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("claims/{claimId:int}/documents")]
        public async Task<IActionResult> GetDocuments(int claimId)
        {
            var claim = await claimRepository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var documents = await documentRepository.GetDocumentsByClaimIdAsync(claimId);
            return Ok(documents);
        }

    [Authorize(Roles = "Admin")]
    [HttpPut("admin/claims/{claimId:int}/documents/verify")]
        public async Task<IActionResult> VerifyDocuments(int claimId, [FromBody] List<int> documentIds)
        {
            var claim = await claimRepository.GetClaimByIdAsync(claimId);
            if (claim == null)
                return NotFound("Claim not found.");

            var documents = await documentRepository.VerifyDocumentsAsync(claimId, documentIds);
            if (documents == null)
                return BadRequest("Provide valid document IDs for this claim.");

            return Ok(documents);
        }
    }
}