using Domain.PessoaAggregate;
using System;
using Xunit;

namespace Tests.TestesUnidade.DomainTestes
{
    [CollectionDefinition(nameof(PessoaCollection))]
    public class PessoaCollection : ICollectionFixture<PessoaTestsFixture>
    { }
    public class PessoaTestsFixture : IDisposable
    {
        public Pessoa GerarPessoa()
        {
            var dataNascimento = DateTime.Parse("01/01/2020");
            var pessoa = new Pessoa("jose", "96123219065", dataNascimento);
            return pessoa;
        }
        public void Dispose()
        {

        }
    }
}
