using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PremiumPayment.Application.DTO
{
    public class VerifyRazorpayPaymentDto
    {
        [Required]
        public long payment_id { get; set; }

        [Required]
        public string razorpay_order_id { get; set; } = string.Empty;

        [Required]
        public string razorpay_payment_id { get; set; } = string.Empty;

        [Required]
        public string razorpay_signature { get; set; } = string.Empty;
    }
}
