using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PremiumPayment.Application.DTO;
using PremiumPayment.Application.Interfaces;
using PremiumPayment.Domain.Entities;
using PremiumPayment.Infrastructure.Data;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
namespace PremiumPayment.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPremiumPaymentRepository _paymentService;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentsController(
            IPremiumPaymentRepository paymentService,
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _paymentService = paymentService;
            _context = context;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(
            [FromBody] CreatePaymentDto request)
        {
            var schedule = await _context.premium_schedule
                .FirstOrDefaultAsync(x =>
                    x.schedule_id == request.schedule_id);

            if (schedule == null)
                return NotFound("Premium schedule not found.");

            if (schedule.status == "Paid" ||
                schedule.status == "Cancelled")
                return BadRequest("This schedule cannot be paid.");

            if (schedule.premium_amount + schedule.late_fee <= 0)
                return BadRequest("Invalid premium amount.");

            if (await _paymentService.ExistsByScheduleId(schedule.schedule_id))
                return Conflict("A payment for this schedule is already in progress or completed.");

            var amount = schedule.premium_amount + schedule.late_fee;

            var keyId = _configuration["Razorpay:KeyId"];
            var keySecret = _configuration["Razorpay:KeySecret"];

            if (string.IsNullOrWhiteSpace(keyId) ||
                string.IsNullOrWhiteSpace(keySecret))
                return StatusCode(500, "Razorpay credentials are not configured.");

            var client = _httpClientFactory.CreateClient();
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{keyId}:{keySecret}"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);

            var orderRequest = new
            {
                amount = (long)(amount * 100),
                currency = "INR",
                receipt = $"schedule_{schedule.schedule_id}_{Guid.NewGuid():N}"
            };

            var json = JsonSerializer.Serialize(orderRequest);

            using var response = await client.PostAsync(
                "https://api.razorpay.com/v1/orders",
                new StringContent(json, Encoding.UTF8, "application/json"));

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode,
                    "Razorpay order creation failed: " + responseBody);

            using var document = JsonDocument.Parse(responseBody);
            var orderId = document.RootElement
                .GetProperty("id").GetString();

            if (string.IsNullOrWhiteSpace(orderId))
                return StatusCode(502, "Razorpay did not return an order ID.");

            var payment = new premium_payment
            {
                schedule_id = schedule.schedule_id,
                policy_id = schedule.policy_id,
                payment_date = DateTime.UtcNow,
                amount_paid = amount,
                payment_mode = request.payment_mode,
                transaction_reference = orderId,
                payment_status = "Initiated"
            };

            await _paymentService.Add(payment);

            return Ok(new
            {
                message = "Razorpay order created. Complete Checkout before verifying payment.",
                payment_id = payment.payment_id,
                razorpay_order_id = orderId,
                razorpay_key_id = keyId,
                amount = orderRequest.amount,
                currency = orderRequest.currency
            });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyPayment(
            [FromBody] VerifyRazorpayPaymentDto request)
        {
            var payment = await _paymentService.GetById(request.payment_id);

            if (payment == null)
                return NotFound("Payment not found.");

            if (payment.payment_status == "Success")
                return BadRequest("Payment has already been verified.");

            if (payment.transaction_reference != request.razorpay_order_id)
                return BadRequest("Razorpay order ID does not match the payment record.");

            var secret = _configuration["Razorpay:KeySecret"];

            if (string.IsNullOrWhiteSpace(secret))
                return StatusCode(500, "Razorpay secret is not configured.");

            var data = $"{request.razorpay_order_id}|{request.razorpay_payment_id}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var expectedSignature = Convert.ToHexString(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(data)))
                .ToLowerInvariant();

            byte[] expectedBytes;
            byte[] receivedBytes;

            try
            {
                expectedBytes = Convert.FromHexString(expectedSignature);
                receivedBytes = Convert.FromHexString(request.razorpay_signature);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid Razorpay signature format.");
            }

            if (expectedBytes.Length != receivedBytes.Length ||
                !CryptographicOperations.FixedTimeEquals(
                    expectedBytes, receivedBytes))
            {
                return BadRequest("Razorpay signature verification failed.");
            }

            payment.transaction_reference = request.razorpay_payment_id;
            payment.payment_status = "Success";
            payment.payment_date = DateTime.UtcNow;

            await _paymentService.Update(payment);

            var schedule = await _context.premium_schedule
                .FirstOrDefaultAsync(x =>
                    x.schedule_id == payment.schedule_id);

            if (schedule != null)
            {
                schedule.status = "Paid";
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                message = "Payment signature verified successfully.",
                payment_id = payment.payment_id,
                payment_status = payment.payment_status
            });
        }

        [HttpGet("{paymentId:long}")]
        public async Task<IActionResult> GetPayment(long paymentId)
        {
            var payment = await _paymentService.GetById(paymentId);

            if (payment == null)
                return NotFound("Payment not found.");

            return Ok(payment);
        }

        [HttpGet("history/{policyId:long}")]
        public async Task<IActionResult> GetPaymentHistory(long policyId)
        {
            var payments = await _paymentService.GetByPolicyId(policyId);
            return Ok(payments);
        }
    }
}
