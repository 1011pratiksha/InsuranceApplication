using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PremiumPayment.Application.DTO
{
    public class CreatePaymentDto
    {
        [Required]
        public long schedule_id { get; set; }

        [Required]
        public string payment_mode { get; set; } = string.Empty;
    }
}
