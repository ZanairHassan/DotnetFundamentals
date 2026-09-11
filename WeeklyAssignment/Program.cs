using WeeklyAssignment.Data;
using WeeklyAssignment.Filters;
using WeeklyAssignment.Middlewares;
using WeeklyAssignment.Repositories.Implementations;
using WeeklyAssignment.Repositories.Interfaces;
using WeeklyAssignment.Services.Implementations;
using WeeklyAssignment.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ActionExecutionLoggingFilter>();

builder.Services.AddSingleton<InMemoryDataStore>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
