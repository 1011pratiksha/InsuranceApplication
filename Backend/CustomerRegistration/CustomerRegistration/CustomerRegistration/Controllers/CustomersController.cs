
using Microsoft.AspNetCore.Mvc;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CustomerRegistration.Controllers
{
  [Route("api/v1/customers")]
  [ApiController]
  public class CustomersController : ControllerBase
  {
    private readonly ICustomerService service;
    private readonly IHttpClientFactory httpClientFactory;

    public CustomersController(
        ICustomerService service,
        IHttpClientFactory httpClientFactory)
    {
      this.service = service;
      this.httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer(CreateCustomerDto dto)
    {
      var customerId = await service.AddCustomer(dto);

      if (customerId == null)
      {
        return BadRequest(new
        {
          message = "Email or mobile already exists"
        });
      }

      var auditSaved = await SaveAuditLog(
          customerId.Value,
          "Create",
          customerId.Value,
          "Customer created successfully");

      if (!auditSaved)
      {
        return StatusCode(502, new
        {
          message = "Customer created, but audit logging failed",
          customerId = customerId.Value
        });
      }

      return Ok(new
      {
        message = "Customer added successfully",
        customerId = customerId.Value
      });
    }

    [HttpGet]
    public async Task<IActionResult> CustomerList()
    {
      var data = await service.FetchCustomers();
      return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> CustomerDetails(int id)
    {
      var data = await service.FindCustomerById(id);

      if (data == null)
      {
        return NotFound(new
        {
          message = "Customer not found"
        });
      }

      return Ok(data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(
        int id,
        UpdateCustomerDto dto)
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

      var auditSaved = await SaveAuditLog(
          id,
          "Update",
          id,
          "Customer updated successfully");

      if (!auditSaved)
      {
        return StatusCode(502, new
        {
          message = "Customer updated, but audit logging failed",
          customerId = id
        });
      }

      return Ok(new { message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
      var message = await service.DeleteCustomer(id);

      if (message == "Customer not found")
      {
        return NotFound(new { message });
      }

      var auditSaved = await SaveAuditLog(
          id,
          "Delete",
          id,
          "Customer deleted successfully");

      if (!auditSaved)
      {
        return StatusCode(502, new
        {
          message = "Customer deleted, but audit logging failed",
          customerId = id
        });
      }

      return Ok(new { message });
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCustomers(string searchText)
    {
      var data = await service.SearchCustomers(searchText);

      return Ok(new
      {
        message = "Search results",
        data
      });
    }

    private async Task<bool> SaveAuditLog(
        int customerId,
        string action,
        int entityId,
        string description)
    {
      try
      {
        var client = httpClientFactory.CreateClient();

        var auditRequest = new
        {
          CustomerId = customerId,
          Action = action,
          EntityName = "Customer",
          EntityId = entityId,
          Description = description
        };

        var response = await client.PostAsJsonAsync(
            "https://localhost:44387/api/AuditLogs/log",
            auditRequest);

        return response.IsSuccessStatusCode;
      }
      catch (HttpRequestException)
      {
        return false;
      }
      catch (TaskCanceledException)
      {
        return false;
      }
    }
  }
}
