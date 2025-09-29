using Testcontainers.PostgreSql;
using Testcontainers.Redis;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;

namespace FenReads.FunctionalTests.Infrastructure;

public class TestEnvironment : IAsyncDisposable
{
    private readonly PostgreSqlContainer _postgresContainer;
    private readonly RedisContainer _redisContainer;
    private readonly IContainer _apiContainer;
    private readonly INetwork _network;

    public string PostgresConnectionString => _postgresContainer.GetConnectionString();
    public string RedisConnectionString => _redisContainer.GetConnectionString();
    public string ApiBaseUrl { get; private set; } = string.Empty;

    public TestEnvironment()
    {
        _network = new NetworkBuilder()
            .WithName($"fenreads-test-{Guid.NewGuid():N}")
            .Build();

        _postgresContainer = new PostgreSqlBuilder()
            .WithDatabase("fenreads_test")
            .WithUsername("fenreads")
            .WithPassword("test_password")
            .WithNetwork(_network)
            .WithNetworkAliases("postgres-test")
            .Build();

        _redisContainer = new RedisBuilder()
            .WithNetwork(_network)
            .WithNetworkAliases("redis-test")
            .Build();

        // Pour l'instant, on utilisera une image pré-construite ou on skippe l'API container
        // car WithDockerfileDirectory n'existe plus dans Testcontainers 4.x
        _apiContainer = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/dotnet/aspnet:9.0")
            .WithNetwork(_network)
            .WithNetworkAliases("fenreads-api")
            .WithPortBinding(5001, true)
            .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Testing")
            .WithEnvironment("ConnectionStrings__DefaultConnection",
                "Host=postgres-test;Database=fenreads_test;Username=fenreads;Password=test_password")
            .WithEnvironment("ConnectionStrings__Redis", "redis-test:6379")
            .WithWaitStrategy(Wait.ForUnixContainer())
            .Build();
    }

    public async Task StartAsync()
    {
        await _network.CreateAsync();

        await Task.WhenAll(
            _postgresContainer.StartAsync(),
            _redisContainer.StartAsync()
        );

        await _apiContainer.StartAsync();

        var mappedPort = _apiContainer.GetMappedPublicPort(80);
        ApiBaseUrl = $"http://localhost:{mappedPort}";
    }

    public async ValueTask DisposeAsync()
    {
        await _apiContainer.DisposeAsync();
        await Task.WhenAll(
            _postgresContainer.DisposeAsync().AsTask(),
            _redisContainer.DisposeAsync().AsTask()
        );
        await _network.DeleteAsync();
    }
}