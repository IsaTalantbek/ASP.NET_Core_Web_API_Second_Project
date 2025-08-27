using Application.Extensions;
using Infrastructure.Extensions;
using API.Middlewares.Logging;
using Microsoft.Extensions.Logging.Console;
using API.Controllers;
using Serilog.Core;
using Serilog;

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

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/api/error");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseLoggingMiddleware();


        app.Run();
    }
}