using API.Application.Commands.PessoaCommand;
using API.Application.Events.PessoaEvent;
using API.Application.Queries;
using Core.Communication.Mediator;
using Domain.PessoaAggregate;
using FluentValidation.Results;
using Infrastructure;
using Infrastructure.Configs;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //commands
            services.AddScoped<IRequestHandler<AdicionarPessoaCommand, ValidationResult>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarPessoaCommand, ValidationResult>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarEnderecoPessoaCommand, ValidationResult>, EnderecoPessoaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarEnderecoPessoaCommand, ValidationResult>, EnderecoPessoaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverEnderecoPessoaCommand, ValidationResult>, EnderecoPessoaCommandHandler>();

            //events
            services.AddScoped<INotificationHandler<PessoaAdicionadaEvent>, PessoaEventHandler>();
            services.AddScoped<INotificationHandler<PessoaAtualizadaEvent>, PessoaEventHandler>();

            //queries
            services.AddScoped<IPessoaQuery, PessoaQuery>();

            //IOptions configs
             services.Configure<MongoConfig>(options => configuration.GetSection(nameof(MongoConfig)).Bind(options));

            //repositorios
            services.AddScoped<IMongoDBContext, MongoDbContext>();
    
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IPessoaMongoRepository, PessoaMongoRepository>();
        }
    }
}
