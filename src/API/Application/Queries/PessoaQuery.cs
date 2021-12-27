using API.Application.DTOs;
using AutoMapper;
using Domain.PessoaAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

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

        public async Task<PessoaDto> ObterPorCPF(string cpf)
        {
            cpf = cpf.ApenasNumeros();
            var pessoa = _pessoaRepository.ObterPorCPF(cpf);
            var pessoaDto = _mapper.Map<PessoaDto>(pessoa);
            return pessoaDto;
        }

        public async Task<PessoaDto> ObterPorId(int id)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(id);
            var pessoaDto = _mapper.Map<PessoaDto>(pessoa);
            return pessoaDto;
        }

        public async Task<IEnumerable<PessoaDto>> ObterTodos()
        {
            var pessoas = await _pessoaRepository.ObterTodosAsync();
            var pessoasDto = _mapper.Map<IEnumerable<PessoaDto>>(pessoas);
            return pessoasDto;
        }
    }
}
