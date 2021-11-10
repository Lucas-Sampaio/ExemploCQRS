using API.Application.Commands.PessoaCommand;
using API.Application.Events.PessoaEvent;
using Core.Communication.Mediator;
using Domain.PessoaAggregate;
using FluentValidation.Results;
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

            //repositorios
            var connection = configuration.GetConnectionString("MongoConnection");
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped((provider) => new PessoaMongoRepository(connection));
        }
    }
}
