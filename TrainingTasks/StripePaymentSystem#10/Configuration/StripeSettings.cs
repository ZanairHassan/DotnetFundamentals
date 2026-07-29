using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Configuration
{
public class StripeSettings
{
        public string SecretKey { get; set; } = string.Empty;

        public string Currency { get; set; } = "usd";
    }
}
