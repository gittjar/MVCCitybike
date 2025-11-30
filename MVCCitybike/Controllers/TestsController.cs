using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace MVCCitybike.Controllers
{
    public class TestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RunTests()
        {
            var output = new StringBuilder();
            var testResults = new TestResultViewModel
            {
                StartTime = DateTime.Now
            };

            try
            {
                // Run dotnet test command
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = "test --no-build --verbosity normal",
                        WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "MVCCitybike.Tests"),
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine($"ERROR: {args.Data}");
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                await process.WaitForExitAsync();

                testResults.EndTime = DateTime.Now;
                testResults.Output = output.ToString();
                testResults.Success = process.ExitCode == 0;
                testResults.ExitCode = process.ExitCode;

                // Parse test results
                ParseTestResults(testResults);
            }
            catch (Exception ex)
            {
                testResults.Success = false;
                testResults.Output = $"Error running tests: {ex.Message}\n\nMake sure the test project exists and is built.";
                testResults.EndTime = DateTime.Now;
            }

            return View("Results", testResults);
        }

        private void ParseTestResults(TestResultViewModel results)
        {
            var lines = results.Output.Split('\n');
            
            foreach (var line in lines)
            {
                if (line.Contains("Passed!"))
                {
                    results.TestsPassed = true;
                }
                
                // Parse test counts
                if (line.Contains("Test Run Successful."))
                {
                    results.Summary = line.Trim();
                }
                
                // Extract total/passed/failed counts
                var match = System.Text.RegularExpressions.Regex.Match(
                    results.Output, 
                    @"Total:\s*(\d+).*?Passed:\s*(\d+).*?Failed:\s*(\d+)",
                    System.Text.RegularExpressions.RegexOptions.Singleline
                );
                
                if (match.Success)
                {
                    results.TotalTests = int.Parse(match.Groups[1].Value);
                    results.PassedTests = int.Parse(match.Groups[2].Value);
                    results.FailedTests = int.Parse(match.Groups[3].Value);
                }
            }
        }
    }

    public class TestResultViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public bool Success { get; set; }
        public int ExitCode { get; set; }
        public string Output { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public bool TestsPassed { get; set; }
        public int TotalTests { get; set; }
        public int PassedTests { get; set; }
        public int FailedTests { get; set; }
    }
}
