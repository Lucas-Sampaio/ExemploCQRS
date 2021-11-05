using API.Application.Commands.PessoaCommand;
using AutoMapper;
using Domain.PessoaAggregate;

namespace API.AutoMapper
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<AdicionarEnderecoPessoaCommand, Endereco>();
            CreateMap<AtualizarPessoaCommand, Endereco>();
        }
    }
}
