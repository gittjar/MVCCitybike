using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcStation.Data;
using MvcBiketripsMay2021.Data;
using System.Globalization;

// MIGRATIONS FOR DB
// dotnet ef migrations add InitMigrate --context MvcBiketripsMay2021Context
// dotnet ef migrations add InitMigrate --context MvcStationContext
//

// Load environment variables from .env file BEFORE creating builder
Console.WriteLine($"🔍 Current Directory: {Directory.GetCurrentDirectory()}");

// Try multiple locations for .env file
string[] envPaths = new[]
{
    Path.Combine(Directory.GetCurrentDirectory(), ".env"),
    Path.Combine(Directory.GetCurrentDirectory(), "..", ".env"),
    Path.Combine(AppContext.BaseDirectory, ".env"),
    Path.Combine(AppContext.BaseDirectory, "..", ".env")
};

bool envLoaded = false;
foreach (var path in envPaths)
{
    Console.WriteLine($"🔍 Checking: {Path.GetFullPath(path)}");
    if (File.Exists(path))
    {
        DotNetEnv.Env.Load(path);
        Console.WriteLine($"✓ Loaded .env from: {Path.GetFullPath(path)}");
        envLoaded = true;
        break;
    }
}

if (!envLoaded)
{
    Console.WriteLine($"❌ ERROR: .env file not found in any of these locations:");
    foreach (var path in envPaths)
    {
        Console.WriteLine($"   - {Path.GetFullPath(path)}");
    }
    throw new FileNotFoundException("Required .env file not found!");
}

// Build connection string from environment variables
var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Debug output
Console.WriteLine("\n=== Environment Variables ===");
Console.WriteLine($"DB_SERVER: '{dbServer ?? "NULL"}'");
Console.WriteLine($"DB_NAME: '{dbName ?? "NULL"}'");
Console.WriteLine($"DB_USER: '{dbUser ?? "NULL"}'");
Console.WriteLine($"DB_PASSWORD: {(string.IsNullOrEmpty(dbPassword) ? "NOT SET" : $"***SET*** (length: {dbPassword.Length})")}");

if (string.IsNullOrEmpty(dbServer) || string.IsNullOrEmpty(dbName) || 
    string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword))
{
    Console.WriteLine("\n❌ ERROR: Missing required database configuration!");
    Console.WriteLine("Required variables in .env file:");
    Console.WriteLine("  - DB_SERVER");
    Console.WriteLine("  - DB_NAME");
    Console.WriteLine("  - DB_USER");
    Console.WriteLine("  - DB_PASSWORD");
    throw new InvalidOperationException("Database configuration missing! Check that .env file contains all required variables.");
}

// Build connection string - escape special characters in password
var connectionString = $"Server={dbServer};Initial Catalog={dbName};User ID={dbUser};Password={dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
Console.WriteLine($"\n🔗 Connection String (password masked): {connectionString.Replace(dbPassword, "***PASSWORD***")}");

// Use SqlServer get Citybiketrips Data
var builder = WebApplication.CreateBuilder(args);

// Configure globalization to use Finnish culture
var cultureInfo = new CultureInfo("fi-FI");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddDbContext<MvcBiketripsMay2021Context>(options =>
    // use here same connection string than bellow StationContext!
    options.UseSqlServer(connectionString ??
    throw new InvalidOperationException("Connection string 'CitybikeDBContext' not found.")));

/* SQLite
builder.Services.AddDbContext<MvcStationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcStationContext")));
*/

// Use SqlServer get Stations Data
builder.Services.AddDbContext<MvcStationContext>(options =>
    options.UseSqlServer(connectionString ??
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

