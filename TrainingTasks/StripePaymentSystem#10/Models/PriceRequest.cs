using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Models
{
    public class PriceRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "usd";
    }
}
