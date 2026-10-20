using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Constants
{
    public static class payment_mode_values
    {
        public const string UPI = "UPI";
        public const string Credit_Card = "Credit Card";
        public const string Debit_Card = "Debit Card";
        public const string Net_Banking = "Net Banking";
        public const string Cash = "Cash";
        public const string Cheque = "Cheque";
        public const string DD = "DD";
        public const string Wallet = "Wallet";

        public static readonly string[] All =
            { UPI, Credit_Card, Debit_Card, Net_Banking, Cash, Cheque, DD, Wallet };
    }

}
