# FILE LOCATION: src/PO.Infrastructure/Migrations/README.md
# DESCRIPTION: Instructions for creating and running database migrations

## Entity Framework Core Migrations

### Creating Migrations

To create a new migration, run the following command from the solution root directory:

```bash
# Add new migration
dotnet ef migrations add InitialCreate --project src/PO.Infrastructure --startup-project src/PO.API

# Add subsequent migrations
dotnet ef migrations add AddPurchaseOrderEntities --project src/PO.Infrastructure --startup-project src/PO.API
```

### Applying Migrations

```bash
# Update database to latest migration
dotnet ef database update --project src/PO.Infrastructure --startup-project src/PO.API

# Update to specific migration
dotnet ef database update MigrationName --project src/PO.Infrastructure --startup-project src/PO.API
```

### Removing Migrations

```bash
# Remove last migration (if not applied to database)
dotnet ef migrations remove --project src/PO.Infrastructure --startup-project src/PO.API
```

### Generate SQL Scripts

```bash
# Generate SQL script for all migrations
dotnet ef migrations script --project src/PO.Infrastructure --startup-project src/PO.API

# Generate SQL script for specific migration range
dotnet ef migrations script FromMigration ToMigration --project src/PO.Infrastructure --startup-project src/PO.API
```

### Database Connection

Make sure to update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PurchaseOrderDB;Uid=root;Pwd=your_password;CharSet=utf8mb4;"
  }
}
```

### Prerequisites

1. MySQL Server 8.0+ installed and running
2. Database user with appropriate permissions
3. .NET 8 SDK installed
4. Entity Framework Core tools installed:

```bash
dotnet tool install --global dotnet-ef
```

### First Time Setup

1. Update connection string in `appsettings.json`
2. Run migration to create database:
   ```bash
   dotnet ef migrations add InitialCreate --project src/PO.Infrastructure --startup-project src/PO.API
   dotnet ef database update --project src/PO.Infrastructure --startup-project src/PO.API
   ```
3. Verify database creation and seed data
4. Start the API application