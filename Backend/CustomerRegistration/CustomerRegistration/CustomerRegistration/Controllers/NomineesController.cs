using Microsoft.AspNetCore.Mvc;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interfaces;
using System.Threading.Tasks;

namespace CustomerRegistration.Controllers
{
    [Route("api/v1/nominees")]
    [ApiController]
    public class NomineesController : ControllerBase
    {
        INomineeService service;

        public NomineesController(INomineeService service)
        {
            this.service = service;
        }

        [HttpPost("/api/v1/customers/{customerId}/nominees")]
        public async Task<IActionResult> AddNominee(int customerId, CreateNomineeDto dto)
        {
            var message = await service.AddNominee(customerId, dto);
            if (message == "Customer not found")
            {
                return NotFound(new { message });
            }
            if (message.Contains("exceeds 100%"))
            {
                return BadRequest(new { message });
            }
            return Ok(new { message });
        }

        [HttpGet("/api/v1/customers/{customerId}/nominees")]
        public async Task<IActionResult> NomineeList(int customerId)
        {
            var data = await service.FetchNomineesByCustomerId(customerId);
            return Ok(data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateNominee(int id, CreateNomineeDto dto)
        {
            var message = await service.UpdateNominee(id, dto);
            if (message == "Nominee not found")
            {
                return NotFound(new { message });
            }
            if (message.Contains("exceeds 100%"))
            {
                return BadRequest(new { message });
            }
            return Ok(new { message });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteNominee(int id)
        {
            var message = await service.DeleteNominee(id);
            if (message == "Nominee not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }
    }
}
