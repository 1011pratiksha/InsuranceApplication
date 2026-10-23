using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.DTO
{
    public class PremiumScheduleRequestDto
    {
        public int PolicyId { get; set; }
        public string PremiumFrequency { get; set; }
        public int PolicyTermInYears { get; set; }
        public decimal AnnualPremium { get; set; }
        public DateTime StartDate { get; set; }
    }
}
