using API.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    //aqui vai ter todos seus metodos de consulta
    public interface IPessoaQuery
    {
        Task<PessoaDto> ObterPorId(int id);
        Task<PessoaDto> ObterPorCPF(string cpf);
        Task<IEnumerable<PessoaDto>> ObterTodos();
    }
}
