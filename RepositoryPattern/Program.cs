using RepositoryPattern.Data;
using RepositoryPattern.Filters;
using RepositoryPattern.Repositories;
using RepositoryPattern.Repositories.Interfaces;
using RepositoryPattern.Services;
using RepositoryPattern.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
builder.Services.AddSingleton<InMemoryDataStore>(); 

builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
