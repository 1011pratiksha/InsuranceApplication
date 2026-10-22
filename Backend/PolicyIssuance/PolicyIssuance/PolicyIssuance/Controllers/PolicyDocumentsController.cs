using Microsoft.AspNetCore.Mvc;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace PolicyIssuance.Controllers
{
    [ApiController]
    public class PolicyDocumentsController : ControllerBase
    {
        private readonly IPolicyDocumentService docService;

        public PolicyDocumentsController(IPolicyDocumentService docService)
        {
           this.docService = docService;
        }

        [HttpGet("api/v1/policies/{id}/document")]
        public async Task<IActionResult> GetPolicyDocument(int id, [FromQuery] bool download = false)
        {
            try
            {
                var doc = await docService.GetPolicyDocumentAsync(id);
                if (doc == null)
                {
                    doc = await docService.GeneratePolicyDocumentAsync(id);
                }
                if (download && doc.FileBytes != null && doc.FileBytes.Length > 0)
                {
                    return File(doc.FileBytes, "text/plain", doc.DocumentName);
                }
                return Ok(doc);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Policy not found"))
                {
                    return NotFound("Policy not found");
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("api/v1/policies/{id}/document/generate")]
        public async Task<IActionResult> GeneratePolicyDocument(int id)
        {
            try
            {
                var doc = await docService.GeneratePolicyDocumentAsync(id);
                return Ok(doc);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Policy not found"))
                {
                    return NotFound("Policy not found");
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("api/v1/policies/{id}/document/regenerate")]
        public async Task<IActionResult> RegeneratePolicyDocument(int id)
        {
            try
            {
                var doc = await docService.RegeneratePolicyDocumentAsync(id);
                return Ok(doc);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Policy not found"))
                {
                    return NotFound("Policy not found");
                }
                return StatusCode(500, ex.Message);
            }
        }
    }
}
