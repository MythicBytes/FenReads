using FenReads.FunctionalTests.Infrastructure;

namespace FenReads.FunctionalTests.Fixtures;

public class FunctionalTestFixture : IAsyncLifetime
{
    public TestEnvironment Environment { get; private set; } = null!;
    public KarateContainer? KarateRunner { get; private set; }

    public async Task InitializeAsync()
    {
        Environment = new TestEnvironment();
        await Environment.StartAsync();
    }

    public async Task<KarateTestResult> RunKarateTestsAsync(
        string featuresPath,
        string reportsPath,
        string? tags = null,
        int? threads = null)
    {
        KarateRunner = new KarateContainer(featuresPath, reportsPath);
        return await KarateRunner.RunTestsAsync(tags, threads);
    }

    public async Task DisposeAsync()
    {
        if (KarateRunner != null)
        {
            await KarateRunner.DisposeAsync();
        }

        await Environment.DisposeAsync();
    }
}