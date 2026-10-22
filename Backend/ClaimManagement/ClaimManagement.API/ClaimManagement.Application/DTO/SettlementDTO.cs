using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Application.DTO
{
    public class SettlementDTO
    {
        [Required]
        [RegularExpression(
            "^(NEFT|RTGS|IMPS|UPI|Cheque)$",
            ErrorMessage = "Payment mode must be NEFT, RTGS, IMPS, UPI, or Cheque.")]
        public string PaymentMode { get; set; } = "";
    }
}