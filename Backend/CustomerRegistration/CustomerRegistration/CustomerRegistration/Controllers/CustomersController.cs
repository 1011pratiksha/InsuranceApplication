using Microsoft.AspNetCore.Mvc;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interfaces;
using System.Threading.Tasks;

namespace CustomerRegistration.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService service;

        public CustomersController(ICustomerService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CreateCustomerDto dto)
        {
            var message = await service.AddCustomer(dto);
            if (message == "Email or mobile already exists")
            {
                return BadRequest(new { message });
            }
            return Ok(new { message });
        }

        [HttpGet]
        public async Task<IActionResult> CustomerList()
        {
            var data = await service.FetchCustomers();
            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> CustomerDetails(int id)
        {
            var data = await service.FindCustomerById(id);
            if (data == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            return Ok(data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto dto)
        {
            var message = await service.UpdateCustomer(id, dto);
            if (message == "Customer not found")
            {
                return NotFound(new { message });
            }
            if (message == "Email or mobile already exists")
            {
                return BadRequest(new { message });
            }
            return Ok(new { message });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var message = await service.DeleteCustomer(id);
            if (message == "Customer not found")
            {
                return NotFound(new { message });
            }
            return Ok(new { message });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchCustomers(string searchText)
        {
            var data = await service.SearchCustomers(searchText);
            return Ok(new { message = "Search results", data });
        }
    }
}
