using System.ComponentModel.DataAnnotations;

namespace PremiumPayment.Application.DTO
    {
        public class ApproveRefundDto
        {
            [Required]
            public long approved_by { get; set; }
        }
    }

