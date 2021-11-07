using API;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tests.Core;
using Tests.TestesIntegracao.Fixture;
using Xunit;

namespace Tests.TestesIntegracao.Api.Test
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PessoaApiTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public PessoaApiTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar pessoa"), TestPriority(1)]
        [Trait("Pessoa", "Integração API - Pessoa")]
        public async Task AdicionarPessoa_NovaPessoa_DeveRetornarComSucesso()
        {
            // Arrange
            var pessoa = new
            {
                nome = "Pessoa teste",
                cpf = "036.244.090-50",
                dataNascimento = DateTime.Today.AddYears(-20)
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa", pessoa);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
