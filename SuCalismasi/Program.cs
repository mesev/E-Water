using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Areas.Admin.Water.Models.AdminContext>(x => x.UseSqlServer("Data Source=DESKTOP-Q0IKKRN\\SQLEXPRESS;Initial Catalog=Water;Integrated Security=True"));
builder.Services.AddDbContext<Water.Models.WaterContext>(x => x.UseSqlServer("Data Source=DESKTOP-Q0IKKRN\\SQLEXPRESS;Initial Catalog=Water;Integrated Security=True"));

builder.Services.AddControllersWithViews();

var app = builder.Build();
CultureInfo culture = new CultureInfo("tr-TR");
culture.NumberFormat.NumberDecimalSeparator = ".";
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"

);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



  

app.Run();
