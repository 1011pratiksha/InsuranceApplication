using Microsoft.AspNetCore.Mvc;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interfaces;
using System.Threading.Tasks;

namespace CustomerRegistration.Controllers
{
    [Route("api/v1/documents")]
    [ApiController]
    public class CustomerDocumentsController : ControllerBase
    {
        ICustomerDocumentService service;

        public CustomerDocumentsController(ICustomerDocumentService service)
        {
            this.service = service;
        }

        [HttpPost("/api/v1/customers/{customerId}/documents")]
        public async Task<IActionResult> UploadDocument(int customerId, UploadDocumentDto dto)
        {
            var message = await service.UploadDocument(customerId, dto);
            if (message == "Customer not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }

        [HttpGet("/api/v1/customers/{customerId}/documents")]
        public async Task<IActionResult> DocumentList(int customerId)
        {
            var data = await service.FetchDocumentsByCustomerId(customerId);
            return Ok(data);
        }

        [HttpPut]
        [Route("{id}/verify")]
        public async Task<IActionResult> VerifyDocument(int id, [FromQuery] string status = "Verified")
        {
            var message = await service.VerifyDocument(id, status);
            if (message == "Document not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var message = await service.DeleteDocument(id);
            if (message == "Document not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }
    }
}
