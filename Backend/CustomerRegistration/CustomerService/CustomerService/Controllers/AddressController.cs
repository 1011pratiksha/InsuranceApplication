using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerRegistration.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressService service;
        public AddressController(IAddressService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("customers/{id}/addresses")]
        public async Task<IActionResult> AddAddress(int id, CustomerAddressDto dto)
        {
            var message = await service.AddAddress(id, dto);
            return Ok(new { message });
        }

        [HttpGet]
        [Route("customers/{id}/addresses")]
        public async Task<IActionResult> AddressList(int id)
        {
            var data = await service.FetchAddresses(id);
            return Ok(data);
        }

        [HttpPut]
        [Route("addresses/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, CustomerAddressDto dto)
        {
            var message = await service.UpdateAddress(addressId, dto);
            return Ok(new { message });
        }

        [HttpDelete]
        [Route("addresses/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var message = await service.DeleteAddress(addressId);
            return Ok(new { message });
        }
    }
}