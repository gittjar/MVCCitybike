using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcStation.Data;
using MvcBiketripsMay2021.Data;


// Use SqlServer get Citybiketrips Data
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcBiketripsMay2021Context>(options =>
    // use here same connection string than bellow StationContext!
    options.UseSqlServer(builder.Configuration.GetConnectionString("CitybikeDBContext") ??
    throw new InvalidOperationException("Connection string 'CitybikeDBContext' not found.")));

/* SQLite
builder.Services.AddDbContext<MvcStationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcStationContext")));
*/

// Use SqlServer get Stations Data
builder.Services.AddDbContext<MvcStationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CitybikeDBContext") ??
    throw new InvalidOperationException("Connection string 'CitybikeDBContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

