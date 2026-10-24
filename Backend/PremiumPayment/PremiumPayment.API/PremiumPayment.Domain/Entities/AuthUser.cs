using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Infrastructure.Entities
{
    public class AuthUser
    {
        public int UserId { get; set; }

        public string Status { get; set; } = string.Empty;

        public int TokenVersion { get; set; }
    }
}