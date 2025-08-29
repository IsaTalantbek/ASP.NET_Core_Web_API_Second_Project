using API.Middlewares.Logging;
using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using AspNet.Security.OAuth.Yandex;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog(logger);

        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddLoggingMiddleware();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });


        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = YandexAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddYandex(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Yandex:ClientId"];
            options.ClientSecret = builder.Configuration["Authentication:Yandex:ClientSecret"];
            options.CallbackPath = "/api/signin-yandex";
            options.Scope.Add("login:info");
            options.SaveTokens = true;
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/api/error");

        app.UseAuthentication();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseLoggingMiddleware();


        app.Run();
    }
}