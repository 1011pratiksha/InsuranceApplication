using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerRegistration.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class NomineeController : ControllerBase
    {
        INomineeService service;

        public NomineeController(INomineeService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("customers/{id}/nominees")]
        public async Task<IActionResult> AddNominee(int id, NomineeDto dto)
        {
            var message = await service.AddNominee(id, dto);
            return Ok(new { message = "Nominee added successfully" });
        }

        [HttpGet]
        [Route("customers/{id}/nominees")]
        public async Task<IActionResult> NomineeList(int id)
        {
            var data = await service.FetchNominees(id);
            return Ok(data);
        }

        [HttpPut]
        [Route("nominees/{nomineeId}")]
        public async Task<IActionResult> UpdateNominee(int nomineeId, NomineeDto dto)
        {
            var message = await service.UpdateNominee(nomineeId, dto);
            return Ok(new { message = "Nominee updated successfully" });
        }

        [HttpDelete]
        [Route("nominees/{nomineeId}")]
        public async Task<IActionResult> DeleteNominee(int nomineeId)
        {
            var message = await service.DeleteNominee(nomineeId);
            return Ok(new { message = "Nominee deleted successfully" });
        }
    }
}