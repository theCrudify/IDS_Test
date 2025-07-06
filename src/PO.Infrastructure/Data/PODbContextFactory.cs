// FILE LOCATION: src/PO.Infrastructure/Data/PODbContextFactory.cs
// DESCRIPTION: Design-time factory for Entity Framework to create DbContext instances during migrations

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PO.Infrastructure.Data;

/// <summary>
/// Design-time factory for creating PODbContext instances for Entity Framework tooling
/// </summary>
public class PODbContextFactory : IDesignTimeDbContextFactory<PODbContext>
{
    public PODbContext CreateDbContext(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PO.API"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        // Get connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        // Create options builder
        var optionsBuilder = new DbContextOptionsBuilder<PODbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new PODbContext(optionsBuilder.Options);
    }
}