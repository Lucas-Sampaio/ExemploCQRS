using Domain.SendWork;
using System.Threading.Tasks;

namespace Domain.PessoaAggregate
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Pessoa Adicionar(Pessoa pessoa);
        Pessoa Atualizar(Pessoa pessoa);
        Task<Pessoa> ObterTodos();
    }
}
