using API.Application.Commands.PessoaCommand;
using Core.Communication.Mediator;
using Domain.PessoaAggregate;
using FluentValidation.Results;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //commands
            services.AddScoped<IRequestHandler<AdicionarPessoaCommand, ValidationResult>, PessoaCommandHandler>();

            //repositorios
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            //EF
            //services.AddScoped<ProjetoContext>();

        }
    }
}
