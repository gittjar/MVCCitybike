# Azure Pipeline Troubleshooting Guide

## Issue: "Could not find service" Error with Windows 2019

### Root Cause
The "Hosted Windows 2019 with VS2019" pool is being deprecated (retirement: January 1, 2026).
Azure DevOps has started brownout periods where this pool is temporarily unavailable.

## Solution 1: Current Implementation ✅
```yaml
pool:
  name: Azure Pipelines
  vmImage: 'windows-latest'
```
This explicitly uses the Azure Pipelines service with the latest Windows image.

## Solution 2: Use Ubuntu (Recommended) 🚀

Ubuntu is faster, more stable, and not affected by Windows pool issues:

```yaml
pool:
  vmImage: 'ubuntu-latest'
```

**To switch to Ubuntu:**
1. Go to Azure DevOps → Pipelines → Edit
2. Change to `azure-pipelines-ubuntu.yml`
3. Or modify the pool section to use `ubuntu-latest`

## Solution 3: GitHub Actions Alternative

If Azure Pipelines continues to have issues, consider GitHub Actions:

Create `.github/workflows/deploy.yml`:
```yaml
name: Deploy to Azure

on:
  push:
    branches: [ develop ]

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
        
    - name: Restore
      run: dotnet restore
      
    - name: Build
      run: dotnet build --configuration Release --no-restore
      
    - name: Test
      run: dotnet test --configuration Release --no-build
      
    - name: Publish
      run: dotnet publish MVCCitybike/MVCCitybike.csproj -c Release -o ./publish
      
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: 'your-app-name'
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: ./publish
```

## Solution 4: Manual Pool Selection in Azure DevOps

1. Go to **Azure DevOps** → **Project Settings** → **Agent pools**
2. Check which pools are available
3. Update pipeline to use an available pool:
   - `Azure Pipelines` (Microsoft-hosted)
   - `windows-2022`
   - `ubuntu-22.04`

## Verify Current Status

Run this command locally to test your build:
```bash
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release --no-build
dotnet publish MVCCitybike/MVCCitybike.csproj -c Release -o ./publish
```

If this works locally, the issue is purely with Azure DevOps pool configuration.

## Recommended Actions

### Immediate (Try Now):
1. ✅ Current fix pushed - uses explicit "Azure Pipelines" pool name
2. ⏳ Wait for next pipeline run
3. 🔍 Check Azure DevOps Pipelines → Recent runs

### If Still Failing:
1. Switch to `azure-pipelines-ubuntu.yml` (faster, more reliable)
2. Or use GitHub Actions (simpler, free minutes)
3. Or manually select pool in Azure DevOps UI

### Long-term:
- Ubuntu pipelines are recommended for .NET Core apps
- Faster build times (30-50% improvement)
- More stable and actively maintained
- Not affected by Windows licensing issues

## Check Azure DevOps Settings

Navigate to: **Azure DevOps** → **Project Settings** → **Service connections**
- Ensure Azure Resource Manager connection exists
- Verify permissions are correct
- Re-authorize if needed

## Contact Microsoft Support

If none of these work, the issue may be with your Azure DevOps organization:
1. Go to Azure DevOps → Help → Contact Support
2. Mention: "Windows 2019 pool service registration error"
3. Reference ID: `f987b69a-d314-468f-aaf8-6137c847a8e0`

---
**Last Updated**: December 3, 2025
**Status**: Fix deployed - using Azure Pipelines pool with windows-latest
