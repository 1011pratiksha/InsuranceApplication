
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PremiumPayment.Application.Interfaces;
using PremiumPayment.Domain.Entities;
using PremiumPayment.Infrastructure.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PremiumPayment.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/payments")]
    public class PaymentReceiptsController : ControllerBase
    {
        private readonly IPremiumPaymentRepository _paymentService;
        private readonly IPaymentReceiptRepository _receiptService;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PaymentReceiptsController(
            IPremiumPaymentRepository paymentService,
            IPaymentReceiptRepository receiptService,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _paymentService = paymentService;
            _receiptService = receiptService;
            _context = context;
            _environment = environment;
        }

        [HttpGet("{paymentId:long}/receipt")]
        public async Task<IActionResult> DownloadReceipt(long paymentId)
        {
            var payment = await _paymentService.GetById(paymentId);

            if (payment == null)
                return NotFound("Payment not found.");

            if (payment.payment_status != "Success")
                return BadRequest("Receipt is available only for successful payments.");

            var receipt = await _receiptService.GetByPaymentId(paymentId);

            if (receipt == null)
                return NotFound("Receipt not found. Generate the receipt first.");

            var filePath = Path.Combine(
                _environment.ContentRootPath,
                receipt.receipt_path);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Receipt PDF file not found. Regenerate the receipt.");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(
                fileBytes,
                "application/pdf",
                $"{receipt.receipt_number}.pdf");
        }

        [HttpPost("{paymentId:long}/receipt/regenerate")]
        public async Task<IActionResult> RegenerateReceipt(long paymentId)
        {
            var payment = await _paymentService.GetById(paymentId);

            if (payment == null)
                return NotFound("Payment not found.");

            if (payment.payment_status != "Success")
                return BadRequest("Receipt can be generated only for successful payments.");

            var receipt = await _receiptService.GetByPaymentId(paymentId);

            if (receipt == null)
            {
                receipt = new payment_receipt
                {
                    payment_id = paymentId,
                    receipt_number = $"RCP-{Guid.NewGuid():N}",
                    receipt_date = DateTime.UtcNow,
                    receipt_path = Path.Combine(
                        "Receipts",
                        $"RCP-{Guid.NewGuid():N}.pdf")
                };

                // Use the same receipt number in the PDF filename and record.
                receipt.receipt_path = Path.Combine(
                    "Receipts",
                    $"{receipt.receipt_number}.pdf");

                await _receiptService.Add(receipt);
            }
            else
            {
                receipt.receipt_date = DateTime.UtcNow;
                await _receiptService.Update(receipt);
            }

            var directoryPath = Path.Combine(
                _environment.ContentRootPath,
                "Receipts");

            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(
                _environment.ContentRootPath,
                receipt.receipt_path);

            QuestPDF.Settings.License = LicenseType.Community;

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text("PREMIUM PAYMENT RECEIPT")
                            .FontSize(20)
                            .Bold();

                        column.Item().Text($"Receipt Number: {receipt.receipt_number}");
                        column.Item().Text($"Receipt Date: {receipt.receipt_date:dd-MMM-yyyy}");
                        column.Item().Text($"Payment ID: {payment.payment_id}");
                        column.Item().Text($"Policy ID: {payment.policy_id}");
                        column.Item().Text($"Schedule ID: {payment.schedule_id}");
                        column.Item().Text($"Payment Mode: {payment.payment_mode}");
                        column.Item().Text($"Transaction Reference: {payment.transaction_reference}");
                        column.Item().Text($"Amount Paid: INR {payment.amount_paid:N2}");
                        column.Item().Text($"Payment Status: {payment.payment_status}");

                        column.Item().PaddingTop(20)
                            .Text("This receipt confirms the successful premium payment.")
                            .Italic();
                    });
                });
            });

            pdf.GeneratePdf(filePath);

            return Ok(new
            {
                message = "Receipt PDF generated successfully.",
                receipt_id = receipt.receipt_id,
                receipt_number = receipt.receipt_number,
                payment_id = receipt.payment_id,
                receipt_date = receipt.receipt_date,
                download_url = $"/api/v1/payments/{paymentId}/receipt"
            });
        }
    }
}
