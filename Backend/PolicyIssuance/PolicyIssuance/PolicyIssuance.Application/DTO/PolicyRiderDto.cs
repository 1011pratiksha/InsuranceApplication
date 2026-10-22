using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class PolicyRiderDto
    {
        public int PolicyRiderId { get; set; }

        public int PolicyId { get; set; }

        [Required]
        public int RiderId { get; set; }

        [Required]
        public decimal RiderPremium { get; set; }
    }
}
