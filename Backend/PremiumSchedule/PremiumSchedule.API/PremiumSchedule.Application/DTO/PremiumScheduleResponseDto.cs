using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumSchedule.Application.DTO
{
    public class PremiumScheduleResponseDto
    {
        public int ScheduleId { get; set; }
        public int PolicyId { get; set; }
        public string PremiumFrequency { get; set; }
        public int InstallmentNo { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal LateFee { get; set; }
        public string Status { get; set; }
    }
}
