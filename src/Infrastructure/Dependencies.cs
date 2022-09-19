using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppDbConnection");
            var dbType = configuration["DatabaseType"]; // sqlite, npgsql, mySql, sqlServer
            switch (dbType)
            {
                
                case "sqlServer":
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString,
                            x => x.MigrationsAssembly("Infrastructure.Data.MSSQL")));
                    break;
                case "cosmos":
                    throw new InvalidOperationException("cosmos is not implemented");
                case "sqlite":

                    services.AddDbContext<AppDbContext>(
                        options => options.UseSqlite("Data Source=database.sqlite", 
                            x => x.MigrationsAssembly("Infrastructure.Data.Sqlite")));

                    break;

                case "inMemory":
                    services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("wave-db"));
                    break;
                case "npgsql":
                    throw new InvalidOperationException("npgsql is not implemented");
                case "mySql":
                    throw new InvalidOperationException("mySql is not implemented");
                default:
                    throw new InvalidOperationException("db provider is not implemented");
            }

            
        }
    }
}
