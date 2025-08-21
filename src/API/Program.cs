using Application.AutoMapper;
using Application.System;
using Application.User.Commands.CreateUser;
using Application.User.Repositories;
using Domain.Users.Services;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        builder.Services.AddAutoMapper(c => c.AddProfile(typeof(MappingProfile)));
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));
        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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