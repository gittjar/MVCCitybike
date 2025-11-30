# 🧪 Automated Testing Dashboard

## Overview
This application includes a built-in testing dashboard that allows you to run automated unit tests directly from the web interface!

## Features

### ✅ What Gets Tested:
- **Station CRUD Operations**: Create, Read, Update, Delete
- **Biketrip Data Management**: Trip validation and calculations
- **Data Filtering**: City-based filtering, station searches
- **Business Logic**: Duration calculations, speed validation
- **Database Operations**: In-memory database testing

### 📊 Test Dashboard Features:
- **One-Click Testing**: Run all tests with a single button click
- **Visual Results**: See pass/fail status with color-coded cards
- **Detailed Output**: View complete test execution logs
- **Performance Metrics**: Track test duration and counts
- **Real-time Feedback**: Loading indicators during test execution

## How to Use

### Local Development:
1. Navigate to http://localhost:5191/Tests
2. Click "Run All Tests" button
3. View results with detailed output

### Azure Deployment:
1. Go to your deployed site: https://citybikemvc.azurewebsites.net/Tests
2. Click "Run All Tests"
3. See live test results

## Test Structure

### MVCCitybike.Tests Project:
```
MVCCitybike.Tests/
├── StationTests.cs          # Station CRUD and filtering tests
├── BiketripTests.cs         # Biketrip validation and logic tests
└── MVCCitybike.Tests.csproj # Test project configuration
```

### Test Categories:

#### Station Tests:
- ✅ CanAddStation
- ✅ CanUpdateStationCapacity
- ✅ CanDeleteStation
- ✅ CanFilterStationsByCity (Helsinki, Espoo, Vantaa)
- ✅ StationRequiresCoordinates

#### Biketrip Tests:
- ✅ CanAddBiketrip
- ✅ TripDurationIsCalculatedCorrectly
- ✅ CanCalculateAverageSpeed (multiple scenarios)
- ✅ CanFilterTripsByStation
- ✅ CanSortTripsByDuration

## Running Tests

### Via Web Interface:
```
Navigate to: /Tests
Click: "Run All Tests"
```

### Via Command Line:
```bash
# Run all tests
cd MVCCitybike.Tests
dotnet test

# Run with detailed output
dotnet test --verbosity detailed

# Run specific test
dotnet test --filter "FullyQualifiedName~StationTests"
```

## Technologies Used

- **xUnit**: Testing framework
- **Moq**: Mocking library
- **InMemory Database**: EF Core In-Memory provider
- **ASP.NET Core MVC Testing**: Integration testing support

## Benefits for Portfolio

✨ Shows you understand:
- Unit testing principles
- Test-Driven Development (TDD)
- Quality assurance practices
- Continuous testing methodology
- Professional development workflows

## CI/CD Integration

The tests automatically run during Azure DevOps pipeline:
```yaml
- task: DotNetCoreCLI@2
  displayName: 'Run Tests'
  inputs:
    command: test
    projects: '**/*Tests.csproj'
```

## Test Data Safety

⚠️ **Important**: Tests use in-memory database
- No impact on production data
- Test data is created and destroyed automatically
- Safe to run anytime, anywhere

## Future Enhancements

Potential additions:
- [ ] Integration tests for controllers
- [ ] Performance/load testing
- [ ] Code coverage reports
- [ ] Automated test scheduling
- [ ] Test history tracking
- [ ] Email notifications for test failures

## Troubleshooting

### Tests Not Running?
1. Ensure test project is built: `dotnet build`
2. Check test project exists in solution
3. Verify NuGet packages are restored

### Tests Failing Locally?
1. Check database connection in tests
2. Verify models match database schema
3. Review test output for specific errors

---

**This testing dashboard makes your application production-ready and shows professional development practices!** 🚀
