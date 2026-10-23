using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.Common
{
    public static class Money
    {
        /// <summary>Razorpay works in the smallest currency unit (paise).</summary>
        public static long ToPaise(decimal rupees)
            => (long)Math.Round(rupees * 100m, 0, MidpointRounding.AwayFromZero);

        public static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

}
