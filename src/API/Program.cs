using Application.Users.AutoMapper;
using Application.System;
using Application.Users.Commands.CreateUser;
using Application.Users.Repositories;
using Domain.Users.Services;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ProjectDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Зависимости домена
        builder.Services.AddSingleton<CreateUserService>();
        builder.Services.AddSingleton<AccountTransferService>();

        // Зависимости приложения
        builder.Services.AddAutoMapper(c => c.AddProfile(typeof(UsersProfile)));
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));
        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();


        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}