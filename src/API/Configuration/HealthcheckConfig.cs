using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration
{
    public static class HealthcheckConfig
    {
        public static void AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("SQLConnection");

            services.AddHealthChecks()
                .AddSqlServer(connection, name: "BancoSQL");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(connection);


        }
        public static void UseCustomHealthCheckConfiguration(this IApplicationBuilder app)
        {

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
        }

    }
}
