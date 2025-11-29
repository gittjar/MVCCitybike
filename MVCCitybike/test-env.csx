// Test script to verify .env loading
using System;
using System.IO;

// Check current directory
Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

// Check for .env in parent
var parentEnv = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
Console.WriteLine($"\nParent .env path: {parentEnv}");
Console.WriteLine($"Parent .env exists: {File.Exists(parentEnv)}");

// Check for .env in current
var currentEnv = Path.Combine(Directory.GetCurrentDirectory(), ".env");
Console.WriteLine($"\nCurrent .env path: {currentEnv}");
Console.WriteLine($"Current .env exists: {File.Exists(currentEnv)}");

// Try loading with DotNetEnv
if (File.Exists(currentEnv))
{
    Console.WriteLine("\n✓ Found .env in current directory");
    DotNetEnv.Env.Load(currentEnv);
}
else if (File.Exists(parentEnv))
{
    Console.WriteLine("\n✓ Found .env in parent directory");
    DotNetEnv.Env.Load(parentEnv);
}

// Check environment variables
Console.WriteLine("\n=== Environment Variables ===");
Console.WriteLine($"DB_SERVER: {Environment.GetEnvironmentVariable("DB_SERVER")}");
Console.WriteLine($"DB_NAME: {Environment.GetEnvironmentVariable("DB_NAME")}");
Console.WriteLine($"DB_USER: {Environment.GetEnvironmentVariable("DB_USER")}");
var pwd = Environment.GetEnvironmentVariable("DB_PASSWORD");
Console.WriteLine($"DB_PASSWORD: {(string.IsNullOrEmpty(pwd) ? "NOT SET" : "***SET***")}");
