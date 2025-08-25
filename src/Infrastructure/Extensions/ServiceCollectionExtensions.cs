using System.Runtime.CompilerServices;
using Application.System;
using Application.Users.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("DefaultConnection string is null");
        return AddServices(services, connectionString);
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        string connectionString)
        => AddServices(services, connectionString);

    private static IServiceCollection AddServices(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ProjectDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}