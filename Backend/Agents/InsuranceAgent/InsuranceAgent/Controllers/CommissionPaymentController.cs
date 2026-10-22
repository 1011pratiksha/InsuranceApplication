using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAgent.Controllers
{
    [Route("api/v1/payments/payouts")]
    [ApiController]
    public class CommissionPaymentController : ControllerBase
    {
        ICommissionPaymentService service;

        public CommissionPaymentController(ICommissionPaymentService service)
        {
            this.service = service;
        }



        [HttpPost]
        public async Task<IActionResult> ProcessPayout(CommissionPaymentRequestDto dto)
        {
            try
            {
                await service.ProcessPayout(dto);

                return Ok("Commission payout processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetPayoutHistory(string? paymentMode,string? status)
        {
            try
            {
                var payments = await service.GetPayoutHistory(paymentMode, status);

                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("/api/v1/agents/{agentId}/payments/payouts")]
        public async Task<IActionResult> GetAgentPayoutHistory(int agentId)
        {
            try
            {
                var payments = await service.GetAgentPayoutHistory(agentId);

                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("{paymentId}/reconcile")]
        public async Task<IActionResult> ReconcilePayment(int paymentId,CommissionPaymentReconcileDto dto)
        {
            try
            {
                await service.ReconcilePayment(paymentId, dto);

                return Ok("Payment reconciled successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
