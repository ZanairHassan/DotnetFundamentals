using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Models
{
    public class TransactionModel
    {
        public string PaymentIntentId { get; set; } = string.Empty;

        public string CustomerId { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string ChargeId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public bool Refunded { get; set; }

        public string ReceiptUrl { get; set; } = string.Empty;
    }
}
