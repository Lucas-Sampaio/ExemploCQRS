using Domain.SeedWork;

namespace Domain.PessoaAggregate
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Pessoa Adicionar(Pessoa pessoa);
        Pessoa Atualizar(Pessoa pessoa);
    }
}
