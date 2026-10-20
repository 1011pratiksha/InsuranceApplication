using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PremiumPayment.Application.DTO;
using PremiumPayment.Application.Interfaces;
using PremiumPayment.Domain.Entities;
using PremiumPayment.Infrastructure.Data;

namespace PremiumPayment.API.Controllers
{
    [ApiController]
    [Route("api/v1/refunds")]
    public class RefundsController : ControllerBase
    {
        private readonly IPremiumRefundRepository _refundService;
        private readonly IPremiumPaymentRepository _paymentService;
        private readonly ApplicationDbContext _context;

        public RefundsController(
            IPremiumRefundRepository refundService,
            IPremiumPaymentRepository paymentService,
            ApplicationDbContext context)
        {
            _refundService = refundService;
            _paymentService = paymentService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRefund(
            [FromBody] CreateRefundDto request)
        {
            var payment = await _paymentService.GetById(request.payment_id);

            if (payment == null)
                return NotFound("Payment not found.");

            if (payment.payment_status != "Success")
                return BadRequest("Only successful payments can be refunded.");

            if (request.refund_amount <= 0)
                return BadRequest("Refund amount must be greater than zero.");

            var existingRefunds = await _context.premium_refund
                .Where(x => x.payment_id == request.payment_id &&
                    (x.refund_status == "Pending" ||
                     x.refund_status == "Approved"))
                .SumAsync(x => (decimal?)x.refund_amount) ?? 0;

            if (existingRefunds + request.refund_amount > payment.amount_paid)
                return BadRequest(
                    "Total pending and approved refunds cannot exceed the payment amount.");

            var refund = new premium_refund
            {
                payment_id = request.payment_id,
                refund_amount = request.refund_amount,
                refund_date = DateTime.UtcNow,
                refund_status = "Pending",
                approved_by = null
            };

            await _refundService.Add(refund);

            return CreatedAtAction(
    nameof(GetRefundById),
                new { refundId = refund.refund_id },
                new
                {
                    message = "Refund request created and is awaiting approval.",
                    refund_id = refund.refund_id,
                    payment_id = refund.payment_id,
                    refund_amount = refund.refund_amount,
                    refund_status = refund.refund_status
                });
        }

        [HttpPut("{refundId:long}/approve")]
        public async Task<IActionResult> ApproveRefund(
            long refundId,
            [FromBody] ApproveRefundDto request)
        {
            var refund = await _refundService.GetById(refundId);

            if (refund == null)
                return NotFound("Refund not found.");

            if (refund.refund_status != "Pending")
                return BadRequest("Only pending refunds can be approved.");

            if (request.approved_by <= 0)
                return BadRequest("A valid approver ID is required.");

            refund.refund_status = "Approved";
            refund.approved_by = request.approved_by;
            refund.refund_date = DateTime.UtcNow;

            await _refundService.Update(refund);

            return Ok(new
            {
                message = "Refund approved successfully.",
                refund_id = refund.refund_id,
                refund_status = refund.refund_status,
                approved_by = refund.approved_by
            });
        }


        [HttpGet("{refundId:long}")]
        public async Task<IActionResult> GetRefundById(long refundId)
        {
            var refund = await _refundService.GetById(refundId);

            if (refund == null)
                return NotFound("Refund not found.");

            return Ok(new
            {
                refund_id = refund.refund_id,
                payment_id = refund.payment_id,
                refund_amount = refund.refund_amount,
                refund_date = refund.refund_date,
                refund_status = refund.refund_status,
                approved_by = refund.approved_by
            });
        }

    }
}
