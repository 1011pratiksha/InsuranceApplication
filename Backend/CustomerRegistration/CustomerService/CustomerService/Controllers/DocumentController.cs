using CustomerRegistration.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerRegistration.Application.DTO;

namespace CustomerRegistration.API.Controllers
{

    [Route("api/v1")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        IDocumentService service;

        public DocumentController(IDocumentService service)
        {
            this.service = service;
        }

        [HttpPost("customers/{id}/documents")]
        [RequestSizeLimit(6000000)]
        public async Task<IActionResult> UploadDocument(int id, [FromForm] UploadDocumentRequest dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                return BadRequest("Select a file");
            }  

            if (dto.File.Length > 5242880)
            {
                return BadRequest("Maximum size is 5 MB");
            }

            using var stream = new MemoryStream();
            await dto.File.CopyToAsync(stream);
            var message = await service.UploadDocument(id, dto.DocumentType, dto.File.FileName, stream.ToArray());
            return Ok(new { message = "Document uploaded successfully" });
        }

        [HttpGet("customers/{id}/documents")]
        public async Task<IActionResult> DocumentList(int id)
        {
            return Ok(await service.FetchDocuments(id));
        }

        [HttpDelete("documents/{documentId}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            var document = await service.DeleteDocument(documentId);
            return Ok(new { message = "Document deleted successfully" });
        }

        [HttpPut("documents/{documentId}/verify")]
        public async Task<IActionResult> VerifyDocument(int documentId)
        {
            var document = await service.VerifyDocument(documentId);
            return Ok(new { message = "Document verified successfully" });
        }
    }
}