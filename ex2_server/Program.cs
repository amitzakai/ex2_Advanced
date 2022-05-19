using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ex2_server.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ex2_serverContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ex2_serverContext") ?? throw new InvalidOperationException("Connection string 'ex2_serverContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
