
using KycVerification.Application.DTO;
using KycVerification.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KycVerification.Controllers
{
    [ApiController]
    [Route("api/v1/kyc")]
    public class KycController : ControllerBase
    {
        private readonly IKycService service;

        public KycController(IKycService service)
        {
            this.service = service;
        }

        // POST: /api/v1/kyc
        // Create a KYC request.
        [HttpPost]
        public async Task<IActionResult> CreateKyc(
            [FromBody] CreateCustomerKycDto dto)
        {
            try
            {
                var result = await service.CreateKycAsync(dto);
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

        // GET: /api/v1/kyc
        // List all KYC requests.
        [HttpGet]
        public async Task<IActionResult> GetAllKyc()
        {
            var result = await service.GetAllKycAsync();
            return Ok(result);
        }

        // GET: /api/v1/kyc/5
        // Get one KYC request.
        [HttpGet("{kycId:int}")]
        public async Task<IActionResult> GetKycById(int kycId)
        {
            var result = await service.GetKycByIdAsync(kycId);

            if (result == null)
                return NotFound(new { message = "KYC request not found." });

            return Ok(result);
        }

        // PUT: /api/v1/kyc/5
        // Update editable KYC details.
        [HttpPut("{kycId:int}")]
        public async Task<IActionResult> UpdateKyc(
            int kycId,
            [FromBody] UpdateCustomerKycDto dto)
        {
            try
            {
                var result = await service.UpdateKycAsync(kycId, dto);

                if (result == null)
                    return NotFound(new { message = "KYC request not found." });

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: /api/v1/kyc/5
        // Delete a KYC request and its related documents/logs.
        [HttpDelete("{kycId:int}")]
        public async Task<IActionResult> DeleteKyc(int kycId)
        {
            try
            {
                bool deleted = await service.DeleteKycAsync(kycId);

                if (!deleted)
                    return NotFound(new { message = "KYC request not found." });

                return Ok(new
                {
                    message = "KYC request deleted successfully."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // POST: /api/v1/kyc/documents
        // Upload a document using multipart/form-data.
        [HttpPost("documents")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadDocument(
            [FromForm] UploadKycDocumentDto dto)
        {
            try
            {
                var result = await service.UploadDocumentAsync(dto);
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

        // GET: /api/v1/kyc/5/documents
        // List documents belonging to a KYC request.
        [HttpGet("{kycId:int}/documents")]
        public async Task<IActionResult> GetDocuments(int kycId)
        {
            var result = await service.GetDocumentsAsync(kycId);
            return Ok(result);
        }

        // PUT: /api/v1/kyc/5/approve
        // Approve a KYC request.
        [HttpPut("{kycId:int}/approve")]
        public async Task<IActionResult> ApproveKyc(
            int kycId, [FromBody] ReviewKycDto dto)
        {
            try
            {
                bool approved = await service.ApproveKycAsync(kycId, dto);

                if (!approved)
                    return NotFound(new { message = "KYC request not found." });

                return Ok(new { message = "KYC approved successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT: /api/v1/kyc/5/reject
        // Reject a KYC request.
        [HttpPut("{kycId:int}/reject")]
        public async Task<IActionResult> RejectKyc(
            int kycId, [FromBody] ReviewKycDto dto)
        {
            try
            {
                bool rejected = await service.RejectKycAsync(kycId, dto);

                if (!rejected)
                    return NotFound(new { message = "KYC request not found." });

                return Ok(new { message = "KYC rejected successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: /api/v1/kyc/5/resubmit
        // Resubmit a rejected KYC request.
        [HttpPut("{kycId:int}/resubmit")]
        public async Task<IActionResult> ResubmitKyc(int kycId)
        {
            try
            {
                bool resubmitted = await service.ResubmitKycAsync(kycId);

                if (!resubmitted)
                    return NotFound(new { message = "KYC request not found." });

                return Ok(new { message = "KYC resubmitted successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT: /api/v1/kyc/documents/5/verify
        // Verify an uploaded document.
        [HttpPut("documents/{documentId:int}/verify")]
        public async Task<IActionResult> VerifyDocument(
            int documentId, [FromBody] ReviewKycDto dto)
        {
            bool verified = await service.VerifyDocumentAsync(documentId, dto);

            if (!verified)
                return NotFound(new { message = "Document not found." });

            return Ok(new { message = "Document verified successfully." });
        }

        // PUT: /api/v1/kyc/documents/5/reject
        // Reject an uploaded document.
        [HttpPut("documents/{documentId:int}/reject")]
        public async Task<IActionResult> RejectDocument(
            int documentId, [FromBody] ReviewKycDto dto)
        {
            try
            {
                bool rejected =
                    await service.RejectDocumentAsync(documentId, dto);

                if (!rejected)
                    return NotFound(new { message = "Document not found." });

                return Ok(new { message = "Document rejected successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: /api/v1/kyc/documents/5/reupload
        // Replace a rejected document.
        [HttpPut("documents/{documentId:int}/reupload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ReuploadDocument(
            int documentId, [FromForm] UploadKycDocumentDto dto)
        {
            try
            {
                var result =
                    await service.ReuploadDocumentAsync(documentId, dto);

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

        // GET: /api/v1/kyc/dashboard
        // Get KYC dashboard counts.
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await service.GetDashboardAsync();
            return Ok(result);
        }
    }
}
