using Domain.PessoaAggregate;
using System.Linq;
using Tests.TestesUnidade.Fixtures;
using Xunit;

namespace Tests.TestesUnidade.InfrastructureTestes
{
    [Collection(nameof(PessoaRepositoryCollection))]
    public class PessoaRepositoryTest
    {
        private readonly PessoaRepositoryFixture _pessoaRepositoryFixture;
        public PessoaRepositoryTest(PessoaRepositoryFixture pessoaRepositoryFixture)
        {
            _pessoaRepositoryFixture = pessoaRepositoryFixture;
        }

        [Fact(DisplayName = "Adicionar pessoa com Sucesso")]
        [Trait("Pessoa", "Pessoa Repositorio Tests")]
        public void PessoaRepository_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var repositorio = _pessoaRepositoryFixture.ObterPessoaRepositorio();
            var pessoa = _pessoaRepositoryFixture.GerarPessoa();

            //act
            repositorio.Adicionar(pessoa);
            repositorio.UnitOfWork.Commit();
            var result = repositorio.Verificar(x => x.Id > 0);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "atualiza pessoa com Sucesso")]
        [Trait("Pessoa", "Pessoa Repositorio Tests")]
        public void PessoaRepository_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var repositorio = _pessoaRepositoryFixture.ObterPessoaRepositorio();
            var pessoa = _pessoaRepositoryFixture.GerarPessoa();

            //act
            repositorio.Adicionar(pessoa);
            repositorio.UnitOfWork.Commit();

            var endereco = new Endereco("rua 03", "60", "87984848", "bairro teste", "cidade teste", "estado teste");
            pessoa.AdicionarEndereco(endereco);
            repositorio.Atualizar(pessoa);
            repositorio.UnitOfWork.Commit();

            var result = repositorio.Verificar(x => x.Enderecos.Any());

            //Assert
            Assert.True(result);
        }
        [Fact(DisplayName = "remove endereco com Sucesso")]
        [Trait("Pessoa", "Pessoa Repositorio Tests")]
        public void PessoaRepository_RemoverEndereco_DeveExecutarComSucesso()
        {
            // Arrange
            var repositorio = _pessoaRepositoryFixture.ObterPessoaRepositorio();
            var pessoa = _pessoaRepositoryFixture.GerarPessoa();

            //act
            repositorio.Adicionar(pessoa);
            var endereco = new Endereco("rua 03", "60", "87984848", "bairro teste", "cidade teste", "estado teste");
            pessoa.AdicionarEndereco(endereco);
            repositorio.UnitOfWork.Commit();

            repositorio.RemoverEndereco(endereco.Id);
            repositorio.UnitOfWork.Commit();

            var result = repositorio.Verificar(x => x.Enderecos.Any(y => y.Id == endereco.Id));

            //Assert
            Assert.False(result);
        }
        [Fact(DisplayName = "busca um endereco com Sucesso")]
        [Trait("Pessoa", "Pessoa Repositorio Tests")]
        public void PessoaRepository_BuscarEndereco_DeveExecutarComSucesso()
        {
            // Arrange
            var repositorio = _pessoaRepositoryFixture.ObterPessoaRepositorio();
            var pessoa = _pessoaRepositoryFixture.GerarPessoa();

            //act
            repositorio.Adicionar(pessoa);

            var endereco = new Endereco("rua 03", "60", "87984848", "bairro teste", "cidade teste", "estado teste");
            pessoa.AdicionarEndereco(endereco);
            repositorio.UnitOfWork.Commit();

            var pessoa2 = repositorio.ObterPorId(pessoa.Id, "Enderecos");

            var result = repositorio.Verificar(x => x.Enderecos.Any());

            //Assert
            Assert.Equal(1, pessoa2.Enderecos.Count);
        }
    }
}
