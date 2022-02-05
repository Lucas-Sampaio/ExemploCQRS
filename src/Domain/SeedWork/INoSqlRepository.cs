using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.SeedWork
{
    public interface INoSqlRepository<TEntidade> where TEntidade : class
    {
        Task AdicionarOuAtualizarAsync(TEntidade entidade);
        Task RemoverAsync(int id);
        Task<TEntidade> ObterPorIdAsync(int id);
        Task<List<TEntidade>> ObterTodosAsync();
    }
}
