# Archived Azure Pipelines Configuration

These Azure Pipeline YAML files have been archived because the project now uses **GitHub Actions** for CI/CD.

## Files in This Directory

- `azure-pipelines.yml` - Original Windows-based pipeline (deprecated due to Windows 2019 retirement)
- `azure-pipelines-ubuntu.yml` - Ubuntu-based alternative (never successfully deployed)

## Why Archived?

Azure Pipelines encountered persistent issues:
- Windows Server 2019 deprecation (retirement: January 1, 2026)
- Service registration errors: `Could not find service 'f987b69a-d314-468f-aaf8-6137c847a8e0'`
- Pool configuration problems with both Windows and Ubuntu hosted agents

## Current Deployment Method

**GitHub Actions** (`.github/workflows/deploy.yml`)
- ✅ Reliable automated deployment
- ✅ Runs tests before deployment (14 unit tests)
- ✅ Deploys to Azure App Service: citybikemvc.azurewebsites.net
- ✅ Triggers on push to `develop` branch

## If You Need to Restore Azure Pipelines

These files are kept for reference. To use them:
1. Copy desired YAML file back to project root
2. Update Azure DevOps pipeline configuration
3. Note: May still encounter Windows 2019 deprecation issues

---
**Archived**: December 3, 2025
**Reason**: Migrated to GitHub Actions
**Status**: Preserved for historical reference
