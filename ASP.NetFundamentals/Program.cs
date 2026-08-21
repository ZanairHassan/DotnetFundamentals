using ASP.NetFundamentals.Configurations;
using ASP.NetFundamentals.Filters;
using ASP.NetFundamentals.Interfaces;
using ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime;
using ASP.NetFundamentals.Middlewares;
using ASP.NetFundamentals.Services;
using ASP.NetFundamentals.Services.TestingDependencyLifeTime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddSingleton<StudentStore>();

builder.Services
    .AddOptions<SecretKey>()
    .Bind(builder.Configuration.GetSection(SecretKey.SectionName))
    .Validate(options => !string.IsNullOrWhiteSpace(options.AppToken), "SecretKey:AppToken is required.")
    .ValidateOnStart();

builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

builder.Services.AddSingleton<IDeveloperService, DeveloperService>();

builder.Services.AddSingleton<ISingletonService, SingletonService>();

builder.Services.AddScoped<IScopedService, ScopedService>();

builder.Services.AddTransient<ITransientService, TransientService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<StudentMiddleware>();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PracticeView}/{action=RenderIndex}/{id?}")
    .WithStaticAssets();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Employee}/{action=Index}/{id?}")
//    .WithStaticAssets();


app.Run();
