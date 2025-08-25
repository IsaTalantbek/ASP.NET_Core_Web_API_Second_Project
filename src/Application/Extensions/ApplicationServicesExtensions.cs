using Application.Services.Retry;
using Application.Users.AutoMapper;
using Application.Users.Commands.CreateUser;
using Domain.Users.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Application.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Зависимости домена
        services.AddDomainServices();

        // Зависимости приложения
        services.AddRetryService(configuration);

        services.AddAutoMapper(c => c.AddProfile(typeof(UsersProfile)));
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

        return services;
    }

    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<CreateUserService>();
        services.AddSingleton<AccountTransferService>();
        return services;
    }
}