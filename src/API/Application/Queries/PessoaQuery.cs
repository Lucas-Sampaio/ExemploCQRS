using API.Application.DTOs;
using AutoMapper;
using Infrastructure.Repositories;
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
    public class PessoaQuery : IPessoaQuery
    {
        private readonly PessoaMongoRepository _pessoaRepository;

        private readonly IMapper _mapper;
        public PessoaQuery(PessoaMongoRepository pessoaRepository, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }
        public Task<PessoaDto> ObterPorId(int id)
        {
            var pessoa = _pessoaRepository.ObterPorId(id);
            var pessoaDto = _mapper.Map<PessoaDto>(pessoa);
            return Task.FromResult(pessoaDto);
        }

        public Task<IEnumerable<PessoaDto>> ObterTodos()
        {
            var pessoas = _pessoaRepository.ObterTodos();
            var pessoasDto = _mapper.Map<IEnumerable<PessoaDto>>(pessoas);
            return Task.FromResult(pessoasDto);
        }
    }
}
