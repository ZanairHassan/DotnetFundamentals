using System;
using System.Collections.Generic;
using System.Text;

namespace StripePaymentSystem_10.Helpers
{
    public static class ConsoleMenu
    {
        public static void Show()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine(" Stripe Transaction Manager");
            Console.WriteLine("======================================");

            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. Create Product");
            Console.WriteLine("3. Create Price");
            Console.WriteLine("4. Add Payment Method");
            Console.WriteLine("5. Create Payment Intent");
            Console.WriteLine("6. Confirm Payment");
            Console.WriteLine("7. Refund Payment");
            Console.WriteLine("8. List Transactions");
            Console.WriteLine("9. Exit");

            Console.Write("\nSelect Option :\t");
        }
    }
}
