using Domain.PessoaAggregate;
using Infrastructure;
using Infrastructure.Configs;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HubEventos.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //IOptions configs
            services.Configure<MongoConfig>(options => configuration.GetSection(nameof(MongoConfig)).Bind(options));
            //services.Configure<AzureCosmoConfig>(options => configuration.GetSection(nameof(AzureCosmoConfig)).Bind(options));

            //repositorios
            services.AddScoped<IMongoDBContext, MongoDbContext>();

            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IPessoaMongoRepository, PessoaMongoRepository>();
            //services.AddScoped<IPessoaCosmoRepository, PessoaCosmoRepository>();
        }
    }
}
