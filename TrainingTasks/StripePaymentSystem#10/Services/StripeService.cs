using Microsoft.Extensions.Options;
using Stripe;
using StripePaymentSystem_10.Configuration;
using StripePaymentSystem_10.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Services
{
    public class StripeService : IStripeService
    {
        private readonly StripeSettings _settings;

        public StripeService(IOptions<StripeSettings> options)
        {
            _settings = options.Value;

            StripeConfiguration.ApiKey = _settings.SecretKey;
        }

        public async Task<Customer> CreateCustomerAsync(CustomerRequest request)
        {
            var options = new CustomerCreateOptions
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            var service = new CustomerService();

            return await service.CreateAsync(options);
        }
        public async Task<Product> CreateProductAsync(ProductRequest request)
        {
            var options = new ProductCreateOptions
            {
                Name = request.Name,
                Description = request.Description
            };

            var service = new ProductService();

            return await service.CreateAsync(options);
        }
        public async Task<Price> CreatePriceAsync(PriceRequest request)
        {
            var options = new PriceCreateOptions
            {
                Product = request.ProductId,

                Currency = request.Currency,

                UnitAmount = (long)(request.Amount * 100)
            };

            var service = new PriceService();

            return await service.CreateAsync(options);
        }

        public async Task<PaymentIntent> CreatePaymentIntentAsync(PaymentIntentRequest request)
        {
            var customerService = new CustomerService();

            var customer = await customerService.GetAsync(request.CustomerId);

            if (string.IsNullOrWhiteSpace(customer.InvoiceSettings.DefaultPaymentMethodId))
            {
                throw new Exception("Customer has no default payment method.");
            }

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(request.Amount * 100),

                Currency = request.Currency,

                Customer = request.CustomerId,

                PaymentMethod = customer.InvoiceSettings.DefaultPaymentMethodId,

                Description = request.Description,

                ConfirmationMethod = "manual",

                Confirm = false,

                PaymentMethodTypes = new List<string>
        {
            "card"
        }
            };

            var service = new PaymentIntentService();

            return await service.CreateAsync(options);
        }

        public async Task<PaymentIntent> ConfirmPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();

            var options = new PaymentIntentConfirmOptions();

            return await service.ConfirmAsync(paymentIntentId, options);
        }

        public async Task<Refund> RefundPaymentAsync(RefundRequest request)
        {
            var paymentIntentService = new PaymentIntentService();

            var paymentIntent = await paymentIntentService.GetAsync(request.PaymentIntentId);

            if (string.IsNullOrWhiteSpace(paymentIntent.LatestChargeId))
                throw new Exception("No charge found for this Payment Intent.");

            var refundOptions = new RefundCreateOptions
            {
                Charge = paymentIntent.LatestChargeId,

                Reason = request.Reason
            };

            var refundService = new RefundService();

            return await refundService.CreateAsync(refundOptions);
        }
        public async Task<List<TransactionModel>> GetTransactionsAsync()
        {
            var paymentIntentService = new PaymentIntentService();

            var customerService = new CustomerService();

            var chargeService = new ChargeService();

            var paymentIntents = await paymentIntentService.ListAsync(
                new PaymentIntentListOptions
                {
                    Limit = 100
                });

            var list = new List<TransactionModel>();

            foreach (var paymentIntent in paymentIntents.Data)
            {
                string customerName = "";

                if (!string.IsNullOrWhiteSpace(paymentIntent.CustomerId))
                {
                    try
                    {
                        var customer =
                            await customerService.GetAsync(paymentIntent.CustomerId);

                        customerName = customer.Name;
                    }
                    catch
                    {
                    }
                }

                string chargeId = "";

                bool refunded = false;

                string receiptUrl = "";

                if (!string.IsNullOrWhiteSpace(paymentIntent.LatestChargeId))
                {
                    try
                    {
                        var charge =
                            await chargeService.GetAsync(paymentIntent.LatestChargeId);

                        chargeId = charge.Id;

                        refunded = charge.Refunded;

                        receiptUrl = charge.ReceiptUrl;
                    }
                    catch
                    {
                    }
                }

                list.Add(new TransactionModel
                {
                    PaymentIntentId = paymentIntent.Id,

                    CustomerId = paymentIntent.CustomerId ?? "",

                    CustomerName = customerName,

                    ChargeId = chargeId,

                    Amount = paymentIntent.Amount / 100M,

                    Currency = paymentIntent.Currency.ToUpper(),

                    Status = paymentIntent.Status,

                    Created = paymentIntent.Created,

                    Refunded = refunded,

                    ReceiptUrl = receiptUrl
                });
            }

            return list.OrderByDescending(x => x.Created).ToList();
        }

        public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethodRequest request)
        {
            var tokenService = new TokenService();

            var token = await tokenService.GetAsync(request.Token);

            if (token.Type != "card")
            {
                throw new Exception("The supplied token is not a card token.");
            }

            var paymentMethodService = new PaymentMethodService();

            var paymentMethod = await paymentMethodService.CreateAsync(
                new PaymentMethodCreateOptions
                {
                    Type = "card",

                    Card = new PaymentMethodCardOptions
                    {
                        Token = request.Token
                    }
                });

            await paymentMethodService.AttachAsync(
                paymentMethod.Id,
                new PaymentMethodAttachOptions
                {
                    Customer = request.CustomerId
                });

            var customerService = new CustomerService();

            await customerService.UpdateAsync(
                request.CustomerId,
                new CustomerUpdateOptions
                {
                    InvoiceSettings = new CustomerInvoiceSettingsOptions
                    {
                        DefaultPaymentMethod = paymentMethod.Id
                    }
                });

            return paymentMethod;
        }

    }
}
