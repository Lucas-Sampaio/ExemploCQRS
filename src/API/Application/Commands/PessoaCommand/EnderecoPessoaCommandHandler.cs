using AutoMapper;
using Core.Communication.Mediator;
using Domain.PessoaAggregate;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands.PessoaCommand
{
    public class EnderecoPessoaCommandHandler :
        IRequestHandler<AdicionarEnderecoPessoaCommand, ValidationResult>,
        IRequestHandler<AtualizarEnderecoPessoaCommand, ValidationResult>,
        IRequestHandler<RemoverEnderecoPessoaCommand, ValidationResult>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public EnderecoPessoaCommandHandler(IPessoaRepository pessoaRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(AdicionarEnderecoPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;
            var pessoa = _pessoaRepository.ObterPorId(request.PessoaId);
            if (pessoa == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("", "Essa pessoa não existe no sistema"));
                return request.ValidationResult;
            }

            var endereco = _mapper.Map<Endereco>(request);
            pessoa.AdicionarEndereco(endereco);
            _pessoaRepository.Atualizar(pessoa);
            _ = await _pessoaRepository.UnitOfWork.Commit();
            return request.ValidationResult;
        }

        public async Task<ValidationResult> Handle(AtualizarEnderecoPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var pessoa = _pessoaRepository.ObterPorId(request.PessoaId);
            if (pessoa == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("", "Essa pessoa não existe no sistema"));
                return request.ValidationResult;
            }

            var endereco = _mapper.Map<Endereco>(request);

            pessoa.AtualizarEndereco(endereco);

            _pessoaRepository.Atualizar(pessoa);
            _ = await _pessoaRepository.UnitOfWork.Commit();
            return request.ValidationResult;
        }

        public async Task<ValidationResult> Handle(RemoverEnderecoPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var pessoa = _pessoaRepository.ObterPorId(request.PessoaId, "Enderecos");

            if (pessoa == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("", "Essa pessoa não existe no sistema"));
                return request.ValidationResult;
            }

            var endereco = pessoa.Enderecos.FirstOrDefault(x => x.Id == request.EnderecoId);
            pessoa.RemoverEndereco(endereco);

            _pessoaRepository.Atualizar(pessoa);
            _ = await _pessoaRepository.UnitOfWork.Commit();
            return request.ValidationResult;
        }
    }
}
