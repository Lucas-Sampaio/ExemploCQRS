using Domain.SeedWork;
using System.Threading.Tasks;

namespace Domain.PessoaAggregate
{
    public interface IPessoaCosmoRepository : INoSqlRepository<PessoaCosmo>
    {
        Task<PessoaCosmo> ObterPorCPFAsync(string cpf);
    }
}
