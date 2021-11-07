using Domain.PessoaAggregate;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Tests.TestesUnidade.Fixtures
{
    [CollectionDefinition(nameof(PessoaRepositoryCollection))]
    public class PessoaRepositoryCollection : ICollectionFixture<PessoaRepositoryFixture>
    {
    }
    /// <summary>
    /// Cria uma pessoa repositoriio fake
    /// com dados na memoria
    /// </summary>
    public class PessoaRepositoryFixture
    {
        public PessoaRepository ObterPessoaRepositorio()
        {
            var options = new DbContextOptionsBuilder<ProjetoContext>()
                .UseInMemoryDatabase(databaseName: "Pessoas").Options;
            var context = new ProjetoContext(options);
            return new PessoaRepository(context);

        }

        public Pessoa GerarPessoa()
        {
            var dataNascimento = DateTime.Parse("01/01/2020");
            var pessoa = new Pessoa("jose", "96123219065", dataNascimento);
            return pessoa;

        }
    }
}
