using Core.Messages.Integration;
using Domain.PessoaAggregate;
using MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HubEventos.Api.Services
{
    public class ConsumerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public ConsumerIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }
        private void SetSubscribers()
        {
            _bus.SubscribeAsync<PessoaCadastradaIntegrationEvent>("PessoaCadastrada",
                async request => await SalvarPessoa(request));
        }
        private async Task SalvarPessoa(PessoaCadastradaIntegrationEvent message)
        {
            //como o background service funciona somente com singleton
            //precisa obter outros tipos de instancia dessa forma
            using var scope = _serviceProvider.CreateScope();
            var pessoaRepository = scope.ServiceProvider.GetRequiredService<IPessoaRepository>();
            var mongoRepository = scope.ServiceProvider.GetRequiredService<IPessoaMongoRepository>();
          
            var pessoa = pessoaRepository.ObterPorId(message.PessoaId, "Enderecos");
            var pessoaDocument = new PessoaDocument
            {
                Id = pessoa.Id,
                Cpf = pessoa.Cpf.ToString(),
                DataNascimento = pessoa.DataNascimento,
                Nome = pessoa.Nome,
                Enderecos = pessoa.Enderecos.Select(x => new EnderecoDocument
                {
                    Bairro = x.Bairro,
                    CEP = x.CEP,
                    Cidade = x.Cidade,
                    Estado = x.Estado,
                    Logradouro = x.Logradouro,
                    Numero = x.Numero,
                    Id = x.Id
                }).ToList()
            };
            await mongoRepository.AdicionarOuAtualizarAsync(pessoaDocument);
        }
    }
}
