
using Microsoft.AspNetCore.Mvc;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Application.Interfaces;
using System.Net.Http.Json;

namespace PolicyIssuance.Controllers
{
  [ApiController]
  [Route("api/v1/policies")]
  public class PoliciesController : ControllerBase
  {
    private readonly IPolicyService service;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<PoliciesController> logger;

    public PoliciesController(
        IPolicyService service,
        IHttpClientFactory httpClientFactory,
        ILogger<PoliciesController> logger)
    {
      this.service = service;
      this.httpClientFactory = httpClientFactory;
      this.logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> IssuePolicy(IssuePolicyDto dto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var result = await service.IssuePolicyAsync(dto);

      await SaveAuditLog(
          result.CustomerId,
          "Create",
          result.PolicyId,
          $"Policy {result.PolicyNumber} issued successfully");

      return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPolicies()
    {
      var result = await service.GetAllPoliciesAsync();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPolicyById(int id)
    {
      var result = await service.GetPolicyByIdAsync(id);

      if (result == null)
      {
        return NotFound("Policy not found");
      }

      return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePolicy(
        int id,
        UpdatePolicyDto dto)
    {
      var result = await service.UpdatePolicyAsync(id, dto);

      if (result == null)
      {
        return NotFound("Policy not found");
      }

      await SaveAuditLog(
          result.CustomerId,
          "Update",
          result.PolicyId,
          $"Policy {result.PolicyNumber} updated successfully");

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePolicy(int id)
    {
      var policy = await service.GetPolicyByIdAsync(id);

      if (policy == null)
      {
        return NotFound("Policy not found");
      }

      var result = await service.DeletePolicyAsync(id);

      if (!result)
      {
        return NotFound("Policy not found");
      }

      await SaveAuditLog(
          policy.CustomerId,
          "Delete",
          policy.PolicyId,
          $"Policy {policy.PolicyNumber} deleted or cancelled");

      return Ok("Policy cancelled successfully");
    }

    private async Task SaveAuditLog(
        int customerId,
        string action,
        int policyId,
        string description)
    {
      try
      {
        var client =
            httpClientFactory.CreateClient("AuditLogs");

        var auditLog = new
        {
          CustomerId = customerId,
          Action = action,
          EntityName = "Policy",
          EntityId = policyId,
          Description = description
        };

        var response = await client.PostAsJsonAsync(
            "api/AuditLogs/log",
            auditLog);

        if (!response.IsSuccessStatusCode)
        {
          logger.LogError(
              "Audit logging failed for PolicyId {PolicyId}. Status: {StatusCode}",
              policyId,
              response.StatusCode);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(
            ex,
            "Unable to send audit log for PolicyId {PolicyId}",
            policyId);
      }
    }
  }
}
