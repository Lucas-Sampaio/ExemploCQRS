using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.PessoaAggregate
{
    public interface IPessoaMongoRepository
    {
        Task AdicionarOuAtualizarAsync(PessoaDocument entidade);
        Task RemoverAsync(int id);
        Task<PessoaDocument> ObterPorIdAsync(int id);
        Task<List<PessoaDocument>> ObterTodosAsync();
        PessoaDocument ObterPorCPF(string cpf);
    }
}
