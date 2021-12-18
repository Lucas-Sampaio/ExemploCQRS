using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace API.Configuration
{
    public static class HealthcheckConfig
    {
        public static void AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSql = configuration.GetConnectionString("SQLConnection");
            var connectionMongo = configuration["MongoConfig:Connection"];

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(connectionSql, name: "BancoSQL",
                tags: new string[] { "db", "sql", "sqlserver", "baseEscrita" }, timeout: TimeSpan.FromSeconds(10))
                .AddMongoDb(connectionMongo, name: "MongoDB",
                tags: new string[] { "mongo", "nosql", "baseLeitura" });


            services.AddHealthChecksUI(setup =>
            {
                setup.SetEvaluationTimeInSeconds(5); //tempo para consultar a api
                setup.SetApiMaxActiveRequests(3); //maximo de tentativas ativas
                setup.MaximumHistoryEntriesPerEndpoint(50); //maximo de logs de estado
            }).AddInMemoryStorage();


        }
        public static void UseCustomHealthCheckConfiguration(this IApplicationBuilder app)
        {

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options => 
            {
                options.UIPath = "/api/hc-ui";
                options.ResourcesPath = $"{options.UIPath}/resources";
                options.UseRelativeApiPath = false;
                options.UseRelativeResourcesPath = false;
                options.UseRelativeWebhookPath = false;
            });
        }

    }
}
