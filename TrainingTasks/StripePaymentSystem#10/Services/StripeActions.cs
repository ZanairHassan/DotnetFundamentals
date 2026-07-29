using Stripe;
using StripePaymentSystem_10.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Services
{
    public class StripeActions
    {
        private readonly IStripeService _stripeService;

        public StripeActions(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        private  void ShowHeader(string title)
        {
            Console.Clear();

            Console.WriteLine(title);

            Console.WriteLine(new string('-', title.Length));

            Console.WriteLine();
        }

        private  void Pause()
        {
            Console.WriteLine();

            Console.Write("Press any key...");

            Console.ReadKey();
        }

        private  string ReadString(string message)
        {
            Console.Write(message);

            return Console.ReadLine() ?? "";
        }

        private  decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                    return value;

                Console.WriteLine("Invalid amount.");
            }
        }

        private  int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;

                Console.WriteLine("Invalid number.");
            }
        }

        private static void ShowStripeError(StripeException ex)
        {
            Console.WriteLine();

            Console.WriteLine("Stripe Error");

            Console.WriteLine(ex.StripeError.Message);
        }

        private static void ShowTestCards()
        {
            Console.WriteLine("Available Test Tokens");
            Console.WriteLine("---------------------");
            Console.WriteLine("tok_visa");
            Console.WriteLine("tok_visa_debit");
            Console.WriteLine("tok_mastercard");
            Console.WriteLine("tok_mastercard_debit");
            Console.WriteLine();
        }

        public async Task CreateCustomerAsync()
        {
            ShowHeader("Create Customer");

            var request = new CustomerRequest
            {
                Name = ReadString("Name :\t"),
                Email = ReadString("Email :\t"),
                Phone = ReadString("Phone :\t")
            };

            try
            {
                var customer = await _stripeService.CreateCustomerAsync(request);

                Console.WriteLine();

                Console.WriteLine("Customer Created Successfully");

                Console.WriteLine("----------------------------");

                Console.WriteLine($"Customer ID : {customer.Id}");
                Console.WriteLine($"Name        : {customer.Name}");
                Console.WriteLine($"Email       : {customer.Email}");
                Console.WriteLine($"Phone       : {customer.Phone}");
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        public async Task CreateProductAsync()
        {
            ShowHeader("Create Product");

            var request = new ProductRequest
            {
                Name = ReadString("Product Name :\t"),
                Description = ReadString("Description :\t")
            };

            try
            {
                var product = await _stripeService.CreateProductAsync(request);

                Console.WriteLine();

                Console.WriteLine("Product Created Successfully");

                Console.WriteLine("----------------------------");

                Console.WriteLine($"Product ID : {product.Id}");
                Console.WriteLine($"Name       : {product.Name}");
                Console.WriteLine($"Active     : {product.Active}");
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }

            Pause();
        }

        public async Task CreatePriceAsync()
        {
            ShowHeader("Create Price");

            string productId = ReadString("Product ID :\t");

            decimal amount = ReadDecimal("Amount :\t");

            string currency = ReadString("Currency (usd) :\t");

            if (string.IsNullOrWhiteSpace(currency))
                currency = "usd";

            var request = new PriceRequest
            {
                ProductId = productId,
                Amount = amount,
                Currency = currency
            };

            try
            {
                var price = await _stripeService.CreatePriceAsync(request);

                Console.WriteLine();

                Console.WriteLine("Price Created Successfully");

                Console.WriteLine("--------------------------");

                Console.WriteLine($"Price ID : {price.Id}");
                Console.WriteLine($"Product  : {price.ProductId}");
                Console.WriteLine($"Amount   : {price.UnitAmountDecimal / 100M} {price.Currency.ToUpper()}");
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }

            Pause();
        }

        public async Task AddPaymentMethodAsync()
        {
            ShowHeader("Add Payment Method");

            string customerId = ReadString("Customer ID :\t");

            ShowTestCards();

            string token = ReadString("Stripe Token :\t");

            var request = new PaymentMethodRequest
            {
                CustomerId = customerId,
                Token = token
            };

            try
            {
                var paymentMethod =
                    await _stripeService.CreatePaymentMethodAsync(request);

                Console.WriteLine();

                Console.WriteLine("Payment Method Added Successfully");

                Console.WriteLine("---------------------------------");

                Console.WriteLine($"Payment Method ID : {paymentMethod.Id}");

                Console.WriteLine($"Customer ID       : {customerId}");

                Console.WriteLine($"Brand             : {paymentMethod.Card.Brand}");

                Console.WriteLine($"Last 4 Digits     : {paymentMethod.Card.Last4}");

                Console.WriteLine($"Expiry            : {paymentMethod.Card.ExpMonth}/{paymentMethod.Card.ExpYear}");
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        public async Task CreatePaymentIntentAsync()
        {
            ShowHeader("Create Payment Intent");

            string customerId = ReadString("Customer ID :\t");

            decimal amount = ReadDecimal("Amount :\t");

            string description = ReadString("Description :\t");

            var request = new PaymentIntentRequest
            {
                CustomerId = customerId,

                Amount = amount,

                Currency = "usd",

                Description = description
            };

            try
            {
                var paymentIntent =
                    await _stripeService.CreatePaymentIntentAsync(request);

                Console.WriteLine();

                Console.WriteLine("Payment Intent Created");

                Console.WriteLine("----------------------");

                Console.WriteLine($"Payment Intent ID : {paymentIntent.Id}");

                Console.WriteLine($"Status            : {paymentIntent.Status}");

                Console.WriteLine($"Customer          : {paymentIntent.CustomerId}");

                Console.WriteLine($"Amount            : {paymentIntent.Amount / 100M}");

                Console.WriteLine($"Currency          : {paymentIntent.Currency.ToUpper()}");

                Console.WriteLine($"Description       : {paymentIntent.Description}");
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        public async Task ConfirmPaymentAsync()
        {
            ShowHeader("Confirm Payment");

            string paymentIntentId = ReadString("Payment Intent ID :\t");

            try
            {
                var payment =
                    await _stripeService.ConfirmPaymentIntentAsync(paymentIntentId);

                Console.WriteLine();

                Console.WriteLine("Payment Confirmed");

                Console.WriteLine("-----------------");

                Console.WriteLine($"Payment Intent : {payment.Id}");

                Console.WriteLine($"Status         : {payment.Status}");

                Console.WriteLine($"Amount         : {payment.Amount / 100M}");

                Console.WriteLine($"Currency       : {payment.Currency.ToUpper()}");

                if (!string.IsNullOrWhiteSpace(payment.LatestChargeId))
                {
                    Console.WriteLine($"Charge ID      : {payment.LatestChargeId}");
                }
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        public async Task RefundPaymentAsync()
        {
            ShowHeader("Refund Payment");

            string paymentIntentId =
                ReadString("Payment Intent ID :\t");

            Console.WriteLine();

            Console.WriteLine("Refund Reasons");

            Console.WriteLine("--------------");

            Console.WriteLine("1. Duplicate");

            Console.WriteLine("2. Fraudulent");

            Console.WriteLine("3. Requested By Customer");

            Console.WriteLine();

            string choice =
                ReadString("Select Reason :\t");

            string reason = choice switch
            {
                "1" => "duplicate",

                "2" => "fraudulent",

                _ => "requested_by_customer"
            };

            var request = new RefundRequest
            {
                PaymentIntentId = paymentIntentId,

                Reason = reason
            };

            try
            {
                var refund =
                    await _stripeService.RefundPaymentAsync(request);

                Console.WriteLine();

                Console.WriteLine("Refund Successful");

                Console.WriteLine("-----------------");

                Console.WriteLine($"Refund ID : {refund.Id}");

                Console.WriteLine($"Status    : {refund.Status}");

                Console.WriteLine($"Amount    : {refund.Amount / 100M}");

                Console.WriteLine($"Currency  : USD");

                Console.WriteLine($"Reason    : {refund.Reason}");
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        public async Task ShowTransactionsAsync()
        {
            ShowHeader("Transactions");

            try
            {
                var transactions = await _stripeService.GetTransactionsAsync();

                if (!transactions.Any())
                {
                    Console.WriteLine("No transactions found.");

                    Pause();

                    return;
                }

                foreach (var transaction in transactions)
                {
                    Console.WriteLine(new string('-', 80));

                    Console.WriteLine($"Payment Intent : {transaction.PaymentIntentId}");

                    Console.WriteLine($"Customer       : {transaction.CustomerName}");

                    Console.WriteLine($"Customer ID    : {transaction.CustomerId}");

                    Console.WriteLine($"Charge ID      : {transaction.ChargeId}");

                    Console.WriteLine($"Amount         : {transaction.Amount} {transaction.Currency}");

                    Console.WriteLine($"Status         : {transaction.Status}");

                    Console.WriteLine($"Created        : {transaction.Created}");

                    Console.WriteLine($"Refunded       : {transaction.Refunded}");

                    Console.WriteLine($"Receipt URL    : {transaction.ReceiptUrl}");
                }

                Console.WriteLine(new string('-', 80));

                Console.WriteLine();

                Console.WriteLine($"Total Transactions : {transactions.Count}");
            }
            catch (StripeException ex)
            {
                ShowStripeError(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Pause();
        }


    }
}
