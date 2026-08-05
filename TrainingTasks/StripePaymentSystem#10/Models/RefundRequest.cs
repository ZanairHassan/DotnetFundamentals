using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Models
{
    public class RefundRequest
    {
        public string PaymentIntentId { get; set; } = string.Empty;

        public string? Reason { get; set; }
    }
}
