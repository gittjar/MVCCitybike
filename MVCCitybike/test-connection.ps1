#!/usr/bin/env pwsh

Write-Host "=== Azure SQL Connection Test ===" -ForegroundColor Cyan

# Load .env file
$envFile = Join-Path $PSScriptRoot ".env"
if (Test-Path $envFile) {
    Write-Host "✓ Found .env file at: $envFile" -ForegroundColor Green
    Get-Content $envFile | ForEach-Object {
        if ($_ -match '^([^#][^=]+)=(.*)$') {
            $name = $matches[1].Trim()
            $value = $matches[2].Trim()
            [Environment]::SetEnvironmentVariable($name, $value, 'Process')
            if ($name -like "*PASSWORD*") {
                Write-Host "  $name = ***" -ForegroundColor Yellow
            } else {
                Write-Host "  $name = $value" -ForegroundColor Yellow
            }
        }
    }
} else {
    Write-Host "✗ .env file not found!" -ForegroundColor Red
    exit 1
}

# Get variables
$server = $env:DB_SERVER
$database = $env:DB_NAME
$username = $env:DB_USER
$password = $env:DB_PASSWORD

Write-Host "`n=== Connection Details ===" -ForegroundColor Cyan
Write-Host "Server: $server"
Write-Host "Database: $database"
Write-Host "Username: $username"
Write-Host "Password Length: $($password.Length) chars"

# Build connection string
$connectionString = "Server=$server;Initial Catalog=$database;User ID=$username;Password=$password;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

Write-Host "`n=== Testing Connection ===" -ForegroundColor Cyan

try {
    # Load SQL Client assembly
    Add-Type -AssemblyName "System.Data.SqlClient"
    
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    
    Write-Host "✓ CONNECTION SUCCESS!" -ForegroundColor Green
    Write-Host "Server Version: $($connection.ServerVersion)"
    
    # Test query
    $command = $connection.CreateCommand()
    $command.CommandText = "SELECT @@VERSION AS Version, DB_NAME() AS CurrentDB"
    $reader = $command.ExecuteReader()
    
    while ($reader.Read()) {
        Write-Host "`nDatabase: $($reader['CurrentDB'])" -ForegroundColor Cyan
        Write-Host "Version: $($reader['Version'])" -ForegroundColor Cyan
    }
    
    $reader.Close()
    $connection.Close()
    
} catch {
    Write-Host "✗ CONNECTION FAILED!" -ForegroundColor Red
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    
    if ($_.Exception.InnerException) {
        Write-Host "Inner Error: $($_.Exception.InnerException.Message)" -ForegroundColor Red
    }
    
    Write-Host "`n=== Troubleshooting ===" -ForegroundColor Yellow
    Write-Host "1. Check Azure SQL firewall allows your IP"
    Write-Host "2. Verify username/password in Azure Portal"
    Write-Host "3. Ensure database name is correct"
    Write-Host "4. Check if server name is correct (tcp: prefix, port 1433)"
    
    # Get current IP
    try {
        $myIp = (Invoke-WebRequest -Uri "https://api.ipify.org" -UseBasicParsing).Content
        Write-Host "`nYour current IP: $myIp" -ForegroundColor Cyan
        Write-Host "Add this IP to Azure SQL firewall if not already added."
    } catch {
        Write-Host "Could not determine your IP address"
    }
    
    exit 1
}
