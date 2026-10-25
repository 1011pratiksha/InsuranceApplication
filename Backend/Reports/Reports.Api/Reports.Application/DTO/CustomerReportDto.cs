using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
   public class CustomerReportDto
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? KycStatus { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? Status { get; set; }
    }
}
