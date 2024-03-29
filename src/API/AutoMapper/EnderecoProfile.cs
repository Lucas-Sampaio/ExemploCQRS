﻿using API.Application.Commands.PessoaCommand;
using API.Application.DTOs;
using AutoMapper;
using Domain.PessoaAggregate;

namespace API.AutoMapper
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<AdicionarEnderecoPessoaCommand, Endereco>();
            CreateMap<AtualizarEnderecoPessoaCommand, Endereco>()
                .ForMember(dest=> dest.Id, opt => opt.MapFrom(src => src.EnderecoId));
            CreateMap<Endereco, EnderecoDocument>();
            CreateMap<Endereco, EnderecoDto>();
            CreateMap<EnderecoDocument, EnderecoDto>();
        }
    }
}
