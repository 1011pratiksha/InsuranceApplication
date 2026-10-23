
using KycVerification.Application.DTO;
using KycVerification.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KycVerification.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KycController : ControllerBase
    {
        private readonly IKycService _kycService;

        public KycController(IKycService kycService)
        {
            _kycService = kycService;
        }

        // POST: api/Kyc
        // Create a new KYC request.
        [HttpPost]
        public async Task<IActionResult> CreateKyc(
            [FromBody] CreateCustomerKycDto dto)
        {
            try
            {
                var result = await _kycService.CreateKycAsync(dto);
                return CreatedAtAction(
                    nameof(GetKycById),
                    new { kycId = result.KycId },
                    result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // GET: api/Kyc
        // Retrieve all KYC requests.
        [HttpGet]
        public async Task<IActionResult> GetAllKyc()
        {
            var result = await _kycService.GetAllKycAsync();
            return Ok(result);
        }

        // GET: api/Kyc/5
        // Retrieve a KYC request by ID.
        [HttpGet("{kycId:int}")]
        public async Task<IActionResult> GetKycById(int kycId)
        {
            var result = await _kycService.GetKycByIdAsync(kycId);

            if (result == null)
                return NotFound(new { message = "KYC record not found." });

            return Ok(result);
        }

        // POST: api/Kyc/documents
        // Upload a document using multipart/form-data.
        [HttpPost("documents")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadDocument(
            [FromForm] UploadKycDocumentDto dto)
        {
            try
            {
                var result = await _kycService.UploadDocumentAsync(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT: api/Kyc/5/approve
        // Approve a KYC request.
        [HttpPut("{kycId:int}/approve")]
        public async Task<IActionResult> ApproveKyc(
            int kycId, [FromBody] ReviewKycDto dto)
        {
            try
            {
                var result = await _kycService.ApproveKycAsync(kycId, dto);

                if (!result)
                    return NotFound(new { message = "KYC record not found." });

                return Ok(new { message = "KYC approved successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT: api/Kyc/5/reject
        // Reject a KYC request.
        [HttpPut("{kycId:int}/reject")]
        public async Task<IActionResult> RejectKyc(
            int kycId, [FromBody] ReviewKycDto dto)
        {
            try
            {
                var result = await _kycService.RejectKycAsync(kycId, dto);

                if (!result)
                    return NotFound(new { message = "KYC record not found." });

                return Ok(new { message = "KYC rejected successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Kyc/5/documents
        // Retrieve all documents for a KYC request.
        [HttpGet("{kycId:int}/documents")]
        public async Task<IActionResult> GetDocuments(int kycId)
        {
            var kyc = await _kycService.GetKycByIdAsync(kycId);

            if (kyc == null)
                return NotFound(new { message = "KYC record not found." });

            var result = await _kycService.GetDocumentsAsync(kycId);
            return Ok(result);
        }

        // PUT: api/Kyc/documents/10/verify
        // Verify a document.
        [HttpPut("documents/{documentId:int}/verify")]
        public async Task<IActionResult> VerifyDocument(
            int documentId, [FromBody] ReviewKycDto dto)
        {
            var result = await _kycService.VerifyDocumentAsync(documentId, dto);

            if (!result)
                return NotFound(new { message = "Document not found." });

            return Ok(new { message = "Document verified successfully." });
        }

        // PUT: api/Kyc/documents/10/reject
        // Reject a document.
        [HttpPut("documents/{documentId:int}/reject")]
        public async Task<IActionResult> RejectDocument(
            int documentId, [FromBody] ReviewKycDto dto)
        {
            try
            {
                var result = await _kycService.RejectDocumentAsync(documentId, dto);

                if (!result)
                    return NotFound(new { message = "Document not found." });

                return Ok(new { message = "Document rejected successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Kyc/documents/10/reupload
        // Replace a rejected document.
        [HttpPut("documents/{documentId:int}/reupload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ReuploadDocument(
            int documentId, [FromForm] UploadKycDocumentDto dto)
        {
            try
            {
                var result = await _kycService.ReuploadDocumentAsync(
                    documentId, dto);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
