dotnet ef migrations add InitialCreate --context AppDbContext --project Infrastructure.Data.Sqlite

Add-Migration InitialCreate -Context AppDbContext -Project Infrastructure.Data.Sqlite -OutputDir Migrations
Add-Migration InitialCreate -Context AppDbContext -Project Infrastructure.Data.MSSQL -OutputDir Migrations

dotnet ef migrations add MyMigration --project ../SqlServerMigrations -- --provider SqlServer