using Stripe;
using StripePaymentSystem_10.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Services
{
    public interface IStripeService
    {
        Task<Customer> CreateCustomerAsync(CustomerRequest request);

        Task<Product> CreateProductAsync(ProductRequest request);

        Task<Price> CreatePriceAsync(PriceRequest request);
        Task<PaymentIntent> CreatePaymentIntentAsync(PaymentIntentRequest request);

        Task<PaymentIntent> ConfirmPaymentIntentAsync(string paymentIntentId);
        Task<Refund> RefundPaymentAsync(RefundRequest request);
        Task<List<TransactionModel>> GetTransactionsAsync();
        Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethodRequest request);
    }
}
