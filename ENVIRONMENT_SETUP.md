# Environment Setup Guide

## Local Development

### .env File
The `.env` file contains sensitive configuration like database connection strings. This file is excluded from git via `.gitignore`.

**Location:** `/MVCCitybike/MVCCitybike/.env` (in the project subfolder where the app runs)

**Note:** The app looks for `.env` in the parent directory first, then in the current directory, so you can place it in either location.

**Format:**
```
# Database Configuration
DB_SERVER=tcp:YOUR_SERVER.database.windows.net,1433
DB_NAME=YOUR_DATABASE
DB_USER=YOUR_USERNAME
DB_PASSWORD=YOUR_PASSWORD

# Local Database Configuration (Optional)
DB_SERVER_LOCAL=localhost,1433
DB_NAME_LOCAL=CityBikeStations
DB_USER_LOCAL=sa
DB_PASSWORD_LOCAL=YOUR_LOCAL_PASSWORD
```

Replace the following placeholders:
- `YOUR_SERVER` - Your Azure SQL Server name (e.g., kingdat4)
- `YOUR_DATABASE` - Your database name (e.g., GreenlizardDb)
- `YOUR_USERNAME` - Your database username
- `YOUR_PASSWORD` - Your database password

## Azure Deployment

### Setting up Environment Variables in Azure App Service

1. **Navigate to Azure Portal**
   - Go to https://portal.azure.com
   - Find your App Service (e.g., citybikemvc)

2. **Configure Application Settings**
   - In the left menu, go to **Settings** > **Configuration**
   - Under **Application settings**, click **+ New application setting**

3. **Add Environment Variables**
   Add the following application settings (one at a time):
   
   - Name: `DB_SERVER`, Value: `tcp:YOUR_SERVER.database.windows.net,1433`
   - Name: `DB_NAME`, Value: `YOUR_DATABASE`
   - Name: `DB_USER`, Value: `YOUR_USERNAME`
   - Name: `DB_PASSWORD`, Value: `YOUR_PASSWORD`
   
   Click **OK** after each one

4. **Save Changes**
   - Click **Save** at the top of the Configuration page
   - Click **Continue** when prompted about restarting the app

### Alternative: Using Azure CLI

```bash
az webapp config appsettings set \
  --name YOUR_APP_NAME \
  --resource-group YOUR_RESOURCE_GROUP \
  --settings ConnectionStrings__CitybikeDBContext="YOUR_CONNECTION_STRING"
```

## Security Notes

⚠️ **Important Security Considerations:**

1. **Never commit `.env` file to git**
   - The `.env` file is already in `.gitignore`
   - Double-check before committing
   - If accidentally committed, rotate all passwords immediately

2. **Rotate passwords regularly**
   - Update both `.env` file locally and Azure App Settings
   - Follow your organization's password policy

3. **Use Azure Key Vault for production**
   - Consider using Azure Key Vault for storing sensitive information in production environments
   - Provides better security and audit logging

4. **Restrict database access**
   - Ensure your Azure SQL database firewall rules only allow necessary IP addresses
   - Use Azure Virtual Networks for better security
   - Enable Azure AD authentication when possible

## Setup Instructions

### 1. Install .NET SDK

**Using Homebrew (macOS):**
```bash
brew install --cask dotnet-sdk
```

**Or download from Microsoft:**
- Visit: https://dotnet.microsoft.com/download
- Download and install the appropriate version for your OS

### 2. Verify Installation
```bash
dotnet --version
```

### 3. Restore Packages
```bash
cd MVCCitybike
dotnet restore
```

### 4. Create .env File
Create a `.env` file in the project root with your database connection string (see format above).

### 5. Run the Application
```bash
dotnet run
```

## Troubleshooting

### Connection string not found
- Verify the `.env` file exists in the correct location (project root)
- Check that `DotNetEnv` package is installed (`dotnet restore`)
- Ensure `DotNetEnv.Env.Load()` is called in `Program.cs`

### Azure deployment issues
- Verify Application Settings are saved in Azure Portal
- Check App Service logs for connection errors
- Ensure SQL Server firewall allows Azure services:
  - Azure Portal > SQL Server > Firewalls and virtual networks
  - Enable "Allow Azure services and resources to access this server"

### Package restore issues
- Clear NuGet cache: `dotnet nuget locals all --clear`
- Delete `bin` and `obj` folders
- Run `dotnet restore` again

## Project Structure

```
MVCCitybike/
├── .env                    # Local environment variables (not in git)
├── .gitignore             # Excludes .env and other sensitive files
├── MVCCitybike/
│   ├── Program.cs         # Loads .env file
│   ├── MVCCitybike.csproj # Includes DotNetEnv package
│   └── ...
└── ENVIRONMENT_SETUP.md   # This file
```

## Additional Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [Azure App Service Configuration](https://docs.microsoft.com/azure/app-service/configure-common)
- [Azure Key Vault](https://docs.microsoft.com/azure/key-vault/)
- [DotNetEnv Library](https://github.com/tonerdo/dotnet-env)
