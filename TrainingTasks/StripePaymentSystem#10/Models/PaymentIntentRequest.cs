using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Models
{
    public class PaymentIntentRequest
    {
        public string CustomerId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "usd";

        public string Description { get; set; } = string.Empty;
    }
}
