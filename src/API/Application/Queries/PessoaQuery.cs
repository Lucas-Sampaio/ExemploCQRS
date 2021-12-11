using API.Application.DTOs;
using AutoMapper;
using Domain.PessoaAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    public class PessoaQuery : IPessoaQuery
    {
        private readonly IPessoaMongoRepository _pessoaRepository;

        private readonly IMapper _mapper;
        public PessoaQuery(IPessoaMongoRepository pessoaRepository, IMapper mapper)
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
