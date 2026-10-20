using System.ComponentModel.DataAnnotations;

namespace PremiumPayment.Application.DTO
{
    public class CreateRefundDto
    {
        [Required]
        public long payment_id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal refund_amount { get; set; }
    }
}