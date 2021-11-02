using AutoMapper;
using Core.Communication.Mediator;
using Domain.PessoaAggregate;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands.PessoaCommand
{
    public class PessoaCommandHandler : IRequestHandler<AdicionarPessoaCommand, ValidationResult>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public PessoaCommandHandler(IPessoaRepository pessoaRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(AdicionarPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var pessoa = _mapper.Map<Pessoa>(request);
            _pessoaRepository.Adicionar(pessoa);
            var sucesso = await _pessoaRepository.UnitOfWork.Commit();
            return request.ValidationResult;
        }
    }
}
