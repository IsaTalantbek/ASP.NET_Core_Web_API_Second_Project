using Application.Extensions;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DepositInAccount;
using Application.Users.Commands.UserTransfer;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

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

        services.AddDbContext<ProjectDbContext>(options =>
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=hello;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

        //var connection = new SqliteConnection("DataSource=:memory:");

        //connection.Open();

        //services.AddSingleton(connection); // регаем connection в DI

        //services.AddDbContext<ProjectDbContext>((sp, options) =>
        //{
        //    var conn = sp.GetRequiredService<SqliteConnection>();
        //    options.UseSqlite(conn).EnableServiceProviderCaching(false); // без retry-стратегии;
        // });

        services.AddApplicationServices();
        services.AddInfrastructureServices();

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

        var balances = new[] { 500, 500 };

        var list = new List<Guid>();

        foreach (var balance in balances)
            list.Add(CreateUser(provider, balance, ct).Result);

        var tasks = Enumerable.Range(0, 10)
            .Select(_ => Task.Run(async () => await ConcurrencyTest1Start(provider, ct, list[0], list[1]))).ToList();

        await Task.WhenAll(tasks);
    }

    private async Task ConcurrencyTest1Start(IServiceProvider provider, CancellationToken ct, Guid to, Guid from)
    {
        await using var scope = provider.CreateAsyncScope();

        scope.ServiceProvider.GetRequiredService<ILogger<Tests>>();

        var h = scope.ServiceProvider.GetRequiredService<AccountTransferCommandHandler>();

        var result = await h.Handle(new AccountTransferCommand(to, from, 100), ct);

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Tests>>();

        var message = result switch
        {
            AccountTransferCommandResult.ConcurrencyException => "Concurrency Exception!",
            _ => result.GetType().FullName
        };

        logger.LogInformation(message);

        if (result is AccountTransferCommandResult.ConcurrencyException)
            scope.ServiceProvider.GetRequiredService<ILogger<Tests>>().LogInformation("Concurrency Exception!");
        else
            scope.ServiceProvider.GetRequiredService<ILogger<Tests>>().LogInformation(result.ToString());
    }

    private async Task<Guid> CreateUser(IServiceProvider provider, long balanceAmount, CancellationToken ct)
    {
        await using var scope = provider.CreateAsyncScope();

        var h = scope.ServiceProvider.GetRequiredService<CreateUserCommandHandler>();

        var result = await h.Handle(new CreateUserCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid().ToString()), ct);

        var r = Assert.IsType<CreateUserCommandResult.Success>(result);

        Deposit(scope.ServiceProvider, r.CreatedAccountGuid, balanceAmount, ct);

        return r.CreatedAccountGuid;
    }

    private async Task Deposit(IServiceProvider provider, Guid accountId, long balanceAmount, CancellationToken ct)
    {
        await using var scope = provider.CreateAsyncScope();
            
        var h = scope.ServiceProvider.GetRequiredService<DepositInAccountCommandHandler>();

        var result = await h.Handle(new DepositInAccountCommand(accountId, balanceAmount), ct);

        Assert.IsType<DepositInAccountCommandResult.Success>(result);
    }
}