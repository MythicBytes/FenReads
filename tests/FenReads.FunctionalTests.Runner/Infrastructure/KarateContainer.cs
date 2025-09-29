using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace FenReads.FunctionalTests.Infrastructure;

public class KarateContainer : IAsyncDisposable
{
    private readonly IContainer _container;
    private readonly string _featuresPath;
    private readonly string _reportsPath;

    public KarateContainer(string featuresPath, string reportsPath)
    {
        _featuresPath = Path.GetFullPath(featuresPath);
        _reportsPath = Path.GetFullPath(reportsPath);

        Directory.CreateDirectory(_reportsPath);

        _container = new ContainerBuilder()
            .WithImage("karatelabs/karate:latest")
            .WithName($"karate-tests-{Guid.NewGuid():N}")
            .WithBindMount(_featuresPath, "/tests")
            .WithBindMount(_reportsPath, "/reports")
            .WithEnvironment("KARATE_ENV", "docker")
            .WithCommand("-o", "/reports", "/tests")
            .WithWaitStrategy(Wait.ForUnixContainer())
            .Build();
    }

    public async Task<KarateTestResult> RunTestsAsync(string? tags = null, int? threads = null)
    {
        var command = new List<string> { "-o", "/reports", "/tests" };

        if (!string.IsNullOrEmpty(tags))
        {
            command.Add("--tags");
            command.Add(tags);
        }

        if (threads.HasValue && threads.Value > 1)
        {
            command.Add("--threads");
            command.Add(threads.Value.ToString());
        }

        await _container.StartAsync();

        var exitCode = await _container.GetExitCodeAsync();
        var logs = await GetContainerLogsAsync();

        return new KarateTestResult
        {
            Success = exitCode == 0,
            ExitCode = (int)exitCode,
            Logs = logs,
            ReportsPath = _reportsPath
        };
    }

    private async Task<string> GetContainerLogsAsync()
    {
        var (stdout, stderr) = await _container.GetLogsAsync();
        return $"STDOUT:\n{stdout}\n\nSTDERR:\n{stderr}";
    }

    public async ValueTask DisposeAsync()
    {
        if (_container != null)
        {
            await _container.DisposeAsync();
        }
    }
}

public class KarateTestResult
{
    public bool Success { get; set; }
    public int ExitCode { get; set; }
    public string Logs { get; set; } = string.Empty;
    public string ReportsPath { get; set; } = string.Empty;
}