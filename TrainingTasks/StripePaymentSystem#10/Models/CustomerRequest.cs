using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Models
{
    public class CustomerRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
