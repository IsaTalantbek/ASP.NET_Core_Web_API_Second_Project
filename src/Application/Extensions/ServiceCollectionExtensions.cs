using System.Reflection;
using Application.Users.AutoMapper;
using Application.Users.Commands.CreateUser;
using Domain.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Зависимости домена
        services.AddDomainServices();

        // Зависимости приложения
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