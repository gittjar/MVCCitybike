===========================================
  MVCCitybike - ASP.NET Core MVC App
===========================================

QUICK START
-----------
1. Create .env file with your database credentials (see format below)
2. Run: cd MVCCitybike && dotnet run
3. Open: http://localhost:5191


FEATURES
--------
✓ CRUD operations for Stations and Biketrips
✓ Sort data by departure/return time
✓ Search stations by cities
✓ Pagination support
✓ Shows user IP address and current time
✓ Connected to Azure SQL
✓ DevOps activated


ENVIRONMENT SETUP
-----------------
Required: .NET 8.0 SDK (LTS)
Install:  brew install --cask dotnet-sdk

Create .env file in /MVCCitybike/MVCCitybike/.env:

DB_SERVER=YOUR_SERVER.database.windows.net
DB_NAME=YOUR_DATABASE
DB_USER=YOUR_USERNAME
DB_PASSWORD=YOUR_PASSWORD


AZURE DEPLOYMENT
----------------
Add Application Settings in Azure Portal:
  App Service → Configuration → Application settings

- DB_SERVER = tcp:YOUR_SERVER.database.windows.net,1433
- DB_NAME = YOUR_DATABASE
- DB_USER = YOUR_USERNAME
- DB_PASSWORD = YOUR_PASSWORD


SECURITY
--------
✓ .env file is gitignored (never commit passwords!)
✓ All credentials use environment variables
✓ appsettings.json has no sensitive data

For production: Use Azure Key Vault


TROUBLESHOOTING
---------------
Connection Error:
  → Verify .env exists in MVCCitybike/MVCCitybike/.env
  → Check Azure SQL firewall allows your IP
  → Verify credentials in Azure Portal

Port Already in Use:
  → Run: lsof -ti :5191 | xargs kill -9

Missing Packages:
  → Run: dotnet restore


TECH STACK
----------
- ASP.NET Core MVC (.NET 8.0 LTS)
- Entity Framework Core 8.0
- Azure SQL Database / SQLite
- Bootstrap 5 (UI)
- X.PagedList (pagination)
- DotNetEnv (environment variables)


USEFUL COMMANDS
---------------
Build:    dotnet build
Run:      dotnet run
Restore:  dotnet restore
Clean:    dotnet clean

Migrations:
  dotnet ef migrations add InitMigrate --context MvcBiketripsMay2021Context
  dotnet ef migrations add InitMigrate --context MvcStationContext