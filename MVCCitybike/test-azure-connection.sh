#!/bin/bash

echo "=== Azure SQL Connection Diagnostic ==="
echo ""

# Check if .env exists
if [ -f ".env" ]; then
    echo "✓ .env file found"
    echo ""
    echo "=== Environment Variables ==="
    source .env
    echo "DB_SERVER: ${DB_SERVER}"
    echo "DB_NAME: ${DB_NAME}"
    echo "DB_USER: ${DB_USER}"
    echo "DB_PASSWORD: ${DB_PASSWORD:0:3}***${DB_PASSWORD: -2}"
    echo "DB_PASSWORD length: ${#DB_PASSWORD}"
else
    echo "✗ .env file not found!"
    exit 1
fi

echo ""
echo "=== Your Current IP Address ==="
MY_IP=$(curl -s https://api.ipify.org)
echo "Your IP: $MY_IP"
echo ""
echo "⚠️  Make sure this IP is added to Azure SQL firewall!"
echo ""

echo "=== Connection String Format ==="
SERVER_CLEAN="${DB_SERVER#tcp:}"
SERVER_CLEAN="${SERVER_CLEAN%,*}"
echo "Server (cleaned): $SERVER_CLEAN"
echo "Full format: Server=${DB_SERVER};Initial Catalog=${DB_NAME};User ID=${DB_USER};Password=***"
echo ""

echo "=== Common Issues & Solutions ==="
echo "1. Firewall Issue:"
echo "   → Azure Portal → SQL Server → Networking → Firewall rules"
echo "   → Add rule: ClientIP = $MY_IP"
echo ""
echo "2. Credentials Issue:"
echo "   → Azure Portal → SQL Server → SQL databases → $DB_NAME"
echo "   → Try 'Query editor' with same credentials"
echo ""
echo "3. Password Special Characters:"
echo "   → If password has #, ensure it's not commented in .env"
echo "   → Current password: ${DB_PASSWORD}"
echo ""

echo "=== Quick Azure CLI Check (if az cli installed) ==="
if command -v az &> /dev/null; then
    echo "Checking if logged into Azure..."
    az account show --query name -o tsv 2>/dev/null || echo "Not logged in to Azure CLI"
else
    echo "Azure CLI not installed (optional)"
fi

echo ""
echo "=== Test Connection with sqlcmd (if installed) ==="
if command -v sqlcmd &> /dev/null; then
    SERVER_FOR_SQLCMD="${DB_SERVER#tcp:}"
    echo "Testing: sqlcmd -S $SERVER_FOR_SQLCMD -d $DB_NAME -U $DB_USER -P *** -Q 'SELECT @@VERSION'"
    sqlcmd -S "$SERVER_FOR_SQLCMD" -d "$DB_NAME" -U "$DB_USER" -P "$DB_PASSWORD" -Q "SELECT @@VERSION" 2>&1 | head -10
else
    echo "sqlcmd not installed (install with: brew install sqlcmd)"
fi
