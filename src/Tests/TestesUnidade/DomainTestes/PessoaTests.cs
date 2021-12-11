using Domain.Exceptions;
using Domain.PessoaAggregate;
using System.Linq;
using Tests.TestesUnidade.Fixtures;
using Xunit;

namespace Tests.TestesUnidade.DomainTestes
{
    [Collection(nameof(PessoaCollection))]
    public class PessoaTests
    {
        //usado para gerar dados que vai ser compartilhado
        private readonly PessoaTestsFixture _pessoaTestsFixture;

        public PessoaTests(PessoaTestsFixture pessoaTestsFixture)
        {
            _pessoaTestsFixture = pessoaTestsFixture;
        }

        [Trait("Pessoa", "Pessoa trait teste")]
        [Fact(DisplayName = "AdicionarEnderecoValido")]
        public void Pessoa_AdicionarEndereco_DeveInserirUmNovoEndereco()
        {
            //Arrange
            var pessoa = _pessoaTestsFixture.GerarPessoa();
            var endereco = new Endereco("rua 03", "60", "87984848", "bairro teste", "cidade teste", "estado teste");

            //Act
            pessoa.AdicionarEndereco(endereco);

            //Assert
            Assert.Contains(endereco, pessoa.Enderecos);
        }

        [Trait("Pessoa", "Pessoa trait teste")]
        [Fact(DisplayName = "AtualizaeEnderecoValido")]
        public void Pessoa_AtualizarEndereco_DeveAtualizarUmEnderecoExistente()
        {
            //Arrange
            var pessoa = _pessoaTestsFixture.GerarPessoa();
            var endereco = new Endereco("rua 03", "60", "87984848", "bairro teste", "cidade teste", "estado teste");
            var endereco2 = new Endereco("rua 04", "70", "87984849", "bairro teste 2", "cidade teste 2", "estado teste 2");

            //Act
            pessoa.AdicionarEndereco(endereco);
            var enderecoExistente = pessoa.Enderecos.First();
            enderecoExistente.Cidade = "São Paulo";
            pessoa.AtualizarEndereco(enderecoExistente);

            //Assert
            Assert.Contains(endereco, pessoa.Enderecos);
            Assert.Equal(1, pessoa.Enderecos.Count);
            Assert.Equal("São Paulo", pessoa.Enderecos.First().Cidade);
            //so atualiza endereço existente
            Assert.Throws<DomainException>(() => pessoa.AtualizarEndereco(endereco2));

        }

        [Trait("Pessoa", "Pessoa trait teste")]
        [Fact(DisplayName = "CpfValido")]
        public void Pessoa_Cpf_ValidaCpf()
        {
            //Arrange
            var cpf1 = "961.232.190-65"; //valido
            var cpf2 = "96123219065"; //valido
            var cpf3 = "961.252.190-45";//invalido

            //Act
            var result1 = CPF.Validar(cpf1);
            var result2 = CPF.Validar(cpf2);
            var result3 = CPF.Validar(cpf3);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.False(result3);
            Assert.Throws<DomainException>(() => new CPF(cpf3));
        }
    }
}
