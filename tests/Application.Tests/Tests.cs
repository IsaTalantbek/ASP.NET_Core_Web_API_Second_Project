using Application.Extensions;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DepositInAccount;
using Application.Users.Commands.UserTransfer;
using Application.Services.Retry;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Tests;

public class Tests
{
    [Fact]
    public async Task ConcurrencyTest1()
    {
        var services = new ServiceCollection();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        //var connection = new SqliteConnection("DataSource=:memory:");

        //connection.Open();

        //services.AddSingleton(connection); // регаем connection в DI

        //services.AddDbContext<ProjectDbContext>((sp, options) =>
        //{
        //    var conn = sp.GetRequiredService<SqliteConnection>();
        //    options.UseSqlite(conn).EnableServiceProviderCaching(false); // без retry-стратегии;
        // });

        services.AddApplicationServices(new RetryPolicy(5, 50));
        services.AddInfrastructureServices("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=hello;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        services.AddScoped<CreateUserCommandHandler>();
        services.AddScoped<AccountTransferCommandHandler>();
        services.AddScoped<DepositInAccountCommandHandler>();

        var cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;

        var provider = services.BuildServiceProvider();

        // using (var scope = provider.CreateScope())
        // {
        //    var ctx = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
        //    ctx.Database.EnsureCreated();
        // }
    }
}