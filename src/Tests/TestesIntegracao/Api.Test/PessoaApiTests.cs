using API;
using API.Application.Commands.PessoaCommand;
using API.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        [Fact(DisplayName = "Adicionar pessoa com sucesso"), TestPriority(1)]
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

        [Fact(DisplayName = "Adicionar pessoa com erro"), TestPriority(2)]
        [Trait("Pessoa", "Integração API - Pessoa")]
        public async Task AdicionarPessoa_NovaPessoa_DeveRetornarComErro()
        {
            // Arrange
            var pessoa = new
            {
                nome = "Pessoa teste",
                cpf = "096.144.090-50",
                dataNascimento = DateTime.Today.AddYears(-20)
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa", pessoa);

            // Assert
            Assert.Throws<HttpRequestException>(() => { postResponse.EnsureSuccessStatusCode(); });
        }
        [Fact(DisplayName = "Buscar pessoa com sucesso"), TestPriority(3)]
        [Trait("Pessoa", "Integração API - Pessoa")]
        public async Task BuscarPessoa_Pessoa_DeveRetornarComSucesso()
        {
            // Arrange
            var pessoas = await _testsFixture.Client.GetFromJsonAsync<IEnumerable<PessoaDto>>("api/Pessoa");
            var pessoaID = pessoas.FirstOrDefault()?.Id;
            var cpf = pessoas.FirstOrDefault()?.Cpf;
            //Act
            var result = pessoas.Any();
            var pessoa = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/{pessoaID.GetValueOrDefault()}");
            var pessoaCpf = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/cpf/{cpf}");
            // Assert
            Assert.True(result);
            Assert.NotNull(pessoa);
            Assert.NotNull(pessoaCpf);
        }

        [Fact(DisplayName = "Atualizar pessoa com sucesso"), TestPriority(4)]
        [Trait("Pessoa", "Integração API - Pessoa")]
        public async Task AtualizarPessoa_Pessoa_DeveRetornarComSucesso()
        {
            // Arrange
            var pessoa = new
            {
                nome = "Pessoa teste atualização",
                cpf = "215.336.080-32",
                dataNascimento = DateTime.Today.AddYears(-20)
            };
            _ = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa", pessoa);
            var pessoaCpf = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/cpf/{pessoa.cpf}");

            pessoaCpf.Nome = "pessoa teste atualização " + DateTime.Now.Millisecond;

            // Act
            var putResponse = await _testsFixture.Client.PutAsJsonAsync("api/Pessoa", pessoaCpf);

            // Assert
            putResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Adicionar/atualizar endereco com sucesso"), TestPriority(5)]
        [Trait("Pessoa", "Integração API - Pessoa")]
        public async Task AtualizarPessoa_AdicionarAtualizarEndereco_DeveRetornarComSucesso()
        {
            // Arrange
            var pessoa = new
            {
                nome = "Pessoa teste adição endereço",
                cpf = "613.435.520-87",
                dataNascimento = DateTime.Today.AddYears(-20)
            };
            _ = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa", pessoa);
            var pessoaCpf = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/cpf/{pessoa.cpf}");
            var command = new AdicionarEnderecoPessoaCommand
            {
                Bairro = "João de Barro",
                CEP = "40351745",
                Cidade = "Bela Vista",
                Estado = "Minas Gerais",
                Logradouro = "rua passo fundo",
                Numero = "70",
                PessoaId = pessoaCpf.Id
            };


            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa/endereco", command);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Atualizar endereco com sucesso"), TestPriority(6)]
        [Trait("Pessoa", "Integração API - Pessoa")]
        public async Task AtualizarPessoa_AtualizarEndereco_DeveRetornarComSucesso()
        {
            // Arrange
            var pessoa = new
            {
                nome = "Pessoa teste atualização endereço",
                cpf = "408.422.940-74",
                dataNascimento = DateTime.Today.AddYears(-20)
            };
            _ = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa", pessoa);
            var pessoaCpf = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/cpf/{pessoa.cpf}");
            var command = new AtualizarEnderecoPessoaCommand
            {
                Bairro = "João de Barro",
                CEP = "4035127",
                Cidade = "Belo Horizonte",
                Estado = "Minas Gerais",
                Logradouro = "rua passo fundo",
                Numero = "70",
                PessoaId = pessoaCpf.Id
            };
            _ = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa/endereco", command);

            //obtem a pessoa atualizada com o endereco novo
            pessoaCpf = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/cpf/{pessoa.cpf}");
            //obtem o id do endereço q sera atualizado
            command.EnderecoId = pessoaCpf.Enderecos.FirstOrDefault(x => x.CEP == command.CEP).Id;

            // Act
            var putResponse = await _testsFixture.Client.PutAsJsonAsync("api/Pessoa/endereco", command);

            // Assert
            putResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Remove endereco com sucesso"), TestPriority(7)]
        [Trait("Pessoa", "Integração API - Pessoa")]
        public async Task AtualizarPessoa_RemoverEndereco_DeveRetornarComSucesso()
        {
            // Arrange
            var pessoa = new
            {
                nome = "Pessoa teste remover endereço",
                cpf = "307.388.930-21",
                dataNascimento = DateTime.Today.AddYears(-20)
            };
            _ = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa", pessoa);
            var pessoaCpf = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/cpf/{pessoa.cpf}");
            var command = new AdicionarEnderecoPessoaCommand
            {
                Bairro = "João de Barro",
                CEP = "4035127",
                Cidade = "Belo Horizonte",
                Estado = "Minas Gerais",
                Logradouro = "rua passo fundo",
                Numero = "70",
                PessoaId = pessoaCpf.Id
            };
            _ = await _testsFixture.Client.PostAsJsonAsync("api/Pessoa/endereco", command);

            //obtem a pessoa atualizada com o endereco novo
            pessoaCpf = await _testsFixture.Client.GetFromJsonAsync<PessoaDto>($"api/Pessoa/cpf/{pessoa.cpf}");

            //obtem o id do endereço q sera atualizado
            var enderecoId = pessoaCpf.Enderecos.FirstOrDefault(x => x.CEP == command.CEP).Id;

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"api/Pessoa/{pessoaCpf.Id}/endereco/{enderecoId}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
