using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PO.Infrastructure.Data;

namespace PO.Infrastructure.Services;

public class DatabaseInitializationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializationService> _logger;

    public DatabaseInitializationService(
        IServiceProvider serviceProvider,
        ILogger<DatabaseInitializationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PODbContext>();

        try
        {
            _logger.LogInformation("Starting database initialization...");

            // Apply migrations (this will create database if it doesn't exist)
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            
            if (pendingMigrations.Any())
            {
                _logger.LogInformation($"Applying {pendingMigrations.Count()} pending migrations...");
                await dbContext.Database.MigrateAsync(cancellationToken);
                _logger.LogInformation("Database migrations applied successfully");
            }
            else
            {
                _logger.LogInformation("No pending migrations found");
                
                // Check if database exists
                var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
                if (canConnect)
                {
                    _logger.LogInformation("Database connection verified");
                }
                else
                {
                    _logger.LogWarning("Cannot connect to database");
                }
            }

            // Verify data exists
            await VerifyAndLogDataCounts(dbContext);

            _logger.LogInformation("Database initialization completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during database initialization");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task VerifyAndLogDataCounts(PODbContext dbContext)
    {
        try
        {
            var roleCounts = await dbContext.Roles.CountAsync();
            var departmentCounts = await dbContext.Departments.CountAsync();
            var userCounts = await dbContext.Users.CountAsync();
            var vendorCounts = await dbContext.Vendors.CountAsync();
            var itemCounts = await dbContext.Items.CountAsync();
            var uomCounts = await dbContext.UOMs.CountAsync();
            var taxCounts = await dbContext.Taxes.CountAsync();
            var divisionCounts = await dbContext.Divisions.CountAsync();
            var approvalMatrixCounts = await dbContext.ApprovalMatrices.CountAsync();
            var poHeaderCounts = await dbContext.POHeaders.CountAsync();
            var poDetailCounts = await dbContext.PODetails.CountAsync();

            _logger.LogInformation("Database data summary:");
            _logger.LogInformation($"  - Roles: {roleCounts}");
            _logger.LogInformation($"  - Departments: {departmentCounts}");
            _logger.LogInformation($"  - Users: {userCounts}");
            _logger.LogInformation($"  - Vendors: {vendorCounts}");
            _logger.LogInformation($"  - Items: {itemCounts}");
            _logger.LogInformation($"  - UOMs: {uomCounts}");
            _logger.LogInformation($"  - Taxes: {taxCounts}");
            _logger.LogInformation($"  - Divisions: {divisionCounts}");
            _logger.LogInformation($"  - Approval Matrices: {approvalMatrixCounts}");
            _logger.LogInformation($"  - Purchase Order Headers: {poHeaderCounts}");
            _logger.LogInformation($"  - Purchase Order Details: {poDetailCounts}");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not verify data counts");
        }
    }
}