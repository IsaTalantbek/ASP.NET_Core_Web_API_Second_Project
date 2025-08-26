using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;

// Используется в ef CLI
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
{
    public ProjectDbContext CreateDbContext(string[] args)
    {
        var APIPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "API"));

        Console.WriteLine($"API (startup) project path: {APIPath}");

        var useConnectionString =
            Path.Combine(APIPath, "appsettings.Development.json", "ConnectionStrings.DefaultConnection");

        Console.WriteLine($"Use {useConnectionString}?");
        Console.WriteLine("Press any key to continue, or press CTRL+C to cancel...");
        Console.ReadKey();

        // Конфигурация для среды разработки
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(APIPath)
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException($"connectionString: {useConnectionString} is null");

        var optionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ProjectDbContext(optionsBuilder.Options);
    }
}