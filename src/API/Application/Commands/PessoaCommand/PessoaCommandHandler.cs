using AutoMapper;
using Core.Communication.Mediator;
using Domain.PessoaAggregate;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands.PessoaCommand
{
    public class PessoaCommandHandler :
        IRequestHandler<AdicionarPessoaCommand, ValidationResult>,
        IRequestHandler<AtualizarPessoaCommand, ValidationResult>
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
            _ = await _pessoaRepository.UnitOfWork.Commit();
            return request.ValidationResult;
        }

        public async Task<ValidationResult> Handle(AtualizarPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            if (_pessoaRepository.Verificar(x => x.Cpf.Numero == request.Cpf && x.Id != request.Id))
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("", "Esse cpf já esta em uso"));
                return request.ValidationResult;
            }

            var pessoa = _mapper.Map<Pessoa>(request);
            _pessoaRepository.Atualizar(pessoa);
            _ = await _pessoaRepository.UnitOfWork.Commit();
            return request.ValidationResult;
        }
    }
}
