using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumSchedule.Application.DTO
{
    public class UpdateInstallmentDto
    {
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
