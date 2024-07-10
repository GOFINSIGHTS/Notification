using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.PostgreSql.Persistance;
using Notification.Service.Abstractions.Interfaces;
using Npgsql;

namespace Notification.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var portString = configuration["PostgresPort"];
            portString = string.IsNullOrEmpty(portString) ? "5432" : portString;
            int port = int.Parse(portString);

            var conStrBuilder = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("NotificationContext"))
            {
                Password = configuration["PostgresPassword"],
                Host = configuration["PostgresHost"],
                Port = port,
                Username = configuration["PostgresUsername"],
                Database = configuration["PostgresDatabase"]
            };
            var workTitleContext = conStrBuilder.ConnectionString;
            services.AddDbContext<NotificationContext>(options => options.UseNpgsql(workTitleContext
                , x => x.MigrationsAssembly("Notification.Infrastructure.PostgreSql")));

            services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<NotificationContext>());

            return services;
        }

        public static async void InitializeInfrastructureServices(this IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NotificationContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}