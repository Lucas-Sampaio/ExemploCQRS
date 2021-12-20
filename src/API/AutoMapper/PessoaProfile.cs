using API.Application.Commands.PessoaCommand;
using API.Application.DTOs;
using API.Application.Events.PessoaEvent;
using AutoMapper;
using Domain.PessoaAggregate;

namespace API.AutoMapper
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<AdicionarPessoaCommand, Pessoa>();
            CreateMap<AtualizarPessoaCommand, Pessoa>();
            CreateMap<Pessoa, PessoaDocument>()
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Numero));
            CreateMap<Pessoa, PessoaDto>();
            CreateMap<PessoaDocument, PessoaDto>();
        }
    }
}
