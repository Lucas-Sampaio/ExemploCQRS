using API.Application.Commands.PessoaCommand;
using AutoMapper;
using Domain.PessoaAggregate;

namespace API.AutoMapper
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<AdicionarPessoaCommand, Pessoa>();
        }
    }
}
