using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using StripePaymentSystem_10.Configuration;
using StripePaymentSystem_10.Configuration;
using StripePaymentSystem_10.Helpers;
using StripePaymentSystem_10.Helpers;
using StripePaymentSystem_10.Models;
using StripePaymentSystem_10.Services;
using StripePaymentSystem_10.Services;

var builder = Host.CreateApplicationBuilder(args);
string projectPath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

string folderPath = Path.Combine(projectPath, "Configuration");
string filePath = Path.Combine(folderPath, "appsettings.json");

builder.Configuration.AddJsonFile(filePath, optional: false, reloadOnChange: true);

builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("Stripe"));

builder.Services.AddSingleton<IStripeService, StripeService>();

builder.Services.AddSingleton<StripeActions>();

var app = builder.Build();

var stripeService = app.Services.GetRequiredService<IStripeService>();

var stripeActions = app.Services.GetRequiredService<StripeActions>();

while (true)
{
    ConsoleMenu.Show();

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            await stripeActions.CreateCustomerAsync();
            break;

        case "2":
            await stripeActions.CreateProductAsync();
            break;

        case "3":
            await stripeActions.CreatePriceAsync();
            break;

        case "4":
            await stripeActions.AddPaymentMethodAsync();
            break;

        case "5":
            await stripeActions.CreatePaymentIntentAsync();
            break;

        case "6":
            await stripeActions.ConfirmPaymentAsync();
            break;

        case "7":
            await stripeActions.RefundPaymentAsync();
            break;

        case "8":
            await stripeActions.ShowTransactionsAsync();
            break;

        case "9":
            return;

        default:
            Console.WriteLine("Invalid Option");
            Console.ReadKey();
            break;
    }
}
