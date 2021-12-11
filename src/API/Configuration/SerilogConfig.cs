using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace API.Configuration
{
    public static class SerilogConfig
    {
        public static void ConfigureSerilog(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
                                   .Enrich.FromLogContext()
                                   .WriteTo.Elasticsearch(ConfigureElasticSink(configuration))
                                   .CreateLogger();
            loggerFactory.AddSerilog();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration)
        {
            var elasticUri = configuration["ElasticConfiguration:Uri"];
            var options = new ElasticsearchSinkOptions(new Uri(elasticUri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"centralizador-logs-{DateTime.UtcNow:yyyy-MM}"
            };
            return options;
        }

    }
}
