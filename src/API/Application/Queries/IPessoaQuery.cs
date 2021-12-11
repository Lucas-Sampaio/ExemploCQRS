using API.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    //aqui vai ter todos seus metodos de consulta
    public interface IPessoaQuery
    {
        Task<PessoaDto> ObterPorId(int id);
        Task<IEnumerable<PessoaDto>> ObterTodos();
    }
}
