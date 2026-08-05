using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Models
{
    public class PaymentMethodRequest
    {
        public string CustomerId { get; set; } = "";

        public string Token { get; set; } = "";
    }
}
