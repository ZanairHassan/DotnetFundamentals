using ASP.NetFundamentals.Configurations;
using ASP.NetFundamentals.Interfaces;
using ASP.NetFundamentals.Middlewares;
using ASP.NetFundamentals.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<StudentStore>();

builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

builder.Services.Configure<SecretKey>(
    builder.Configuration.GetSection("SecretKey"));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseMiddleware<StudentMiddleware>();

app.UseRouting();

app.UseAuthorization();

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
