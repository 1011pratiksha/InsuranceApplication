using Microsoft.AspNetCore.Mvc;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interfaces;
using System.Threading.Tasks;

namespace CustomerRegistration.Controllers
{
    [Route("api/v1/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        IAddressService service;

        public AddressesController(IAddressService service)
        {
            this.service = service;
        }

        [HttpPost("/api/v1/customers/{customerId}/addresses")]
        public async Task<IActionResult> AddAddress(int customerId, CreateAddressDto dto)
        {
            var message = await service.AddAddress(customerId, dto);
            if (message == "Customer not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }

        [HttpGet("/api/v1/customers/{customerId}/addresses")]
        public async Task<IActionResult> AddressList(int customerId)
        {
            var data = await service.FetchAddressesByCustomerId(customerId);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, CreateAddressDto dto)
        {
            var message = await service.UpdateAddress(id, dto);
            if (message == "Address not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var message = await service.DeleteAddress(id);
            if (message == "Address not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }
    }
}
