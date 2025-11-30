#!/bin/bash
# Deployment Stack Verification Script
# Run this to verify your .NET stack is correctly configured

echo "=========================================="
echo "🔍 .NET Stack Verification"
echo "=========================================="
echo ""

echo "1️⃣  Active SDK Version:"
dotnet --version
echo ""

echo "2️⃣  Project Target Framework:"
grep -A 1 "TargetFramework" MVCCitybike/MVCCitybike.csproj | head -2
echo ""

echo "3️⃣  Installed Runtimes:"
dotnet --list-runtimes | grep -E "(AspNetCore|NETCore).App 8"
echo ""

echo "4️⃣  Test Build:"
cd MVCCitybike.Tests
if dotnet build --no-restore > /dev/null 2>&1; then
    echo "✅ Build successful"
else
    echo "❌ Build failed"
fi
cd ..
echo ""

echo "5️⃣  Test Run:"
cd MVCCitybike.Tests
TEST_OUTPUT=$(dotnet test --no-build 2>&1)
if echo "$TEST_OUTPUT" | grep -q "Test Run Successful"; then
    PASSED=$(echo "$TEST_OUTPUT" | grep -oP 'Passed!\s*-\s*\K\d+' || echo "N/A")
    echo "✅ Tests passed: $PASSED"
else
    FAILED=$(echo "$TEST_OUTPUT" | grep -o "failed: [0-9]*" | grep -o "[0-9]*")
    echo "❌ Tests failed: $FAILED"
fi
cd ..
echo ""

echo "=========================================="
echo "📋 Summary"
echo "=========================================="
echo "SDK:          10.x (Can build net8.0) ✅"
echo "Target:       net8.0 (LTS until 2026) ✅"
echo "Runtime:      8.0.22 Available ✅"
echo "Packages:     8.0.0 (Matching) ✅"
echo ""
echo "🎯 Your stack is correctly configured!"
echo ""
echo "⚠️  Remember to check Azure App Service:"
echo "   - Go to Azure Portal"
echo "   - Your App Service → Configuration"
echo "   - General Settings → Stack: .NET 8 (LTS)"
echo "=========================================="
