using API.Application.Events.PessoaEvent;
using AutoMapper;
using Core.Communication.Mediator;
using Core.Messages;
using Domain.PessoaAggregate;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands.PessoaCommand
{
    public class PessoaCommandHandler : CommandHandler,
        IRequestHandler<AdicionarPessoaCommand, ValidationResult>,
        IRequestHandler<AtualizarPessoaCommand, ValidationResult>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public PessoaCommandHandler(IPessoaRepository pessoaRepository, IMediatorHandler mediatorHandler, IMapper mapper) : base(mediatorHandler)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(AdicionarPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var pessoa = _mapper.Map<Pessoa>(request);
            if (!ValidarPessoa(pessoa)) return ValidationResult;
            _pessoaRepository.Adicionar(pessoa);

            _ = await _pessoaRepository.UnitOfWork.Commit();

            //evento sera publicado caso salve com sucesso
            var pessoaEvent = new PessoaAdicionadaEvent
            {
                Id = pessoa.Id,
                Cpf = pessoa.Cpf.ToString(),
                DataNascimento = pessoa.DataNascimento,
                Nome = pessoa.Nome
            };

            AdicionarEvento(pessoaEvent);
            await PublicarEventos();

            return request.ValidationResult;
        }

        public async Task<ValidationResult> Handle(AtualizarPessoaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var pessoa = _mapper.Map<Pessoa>(request);
            if (!ValidarPessoa(pessoa)) return ValidationResult;

            _pessoaRepository.Atualizar(pessoa);
            _ = await _pessoaRepository.UnitOfWork.Commit();

            //evento sera publicado caso salve com sucesso
            var pessoaEvent = new PessoaAtualizadaEvent(pessoa.Id);

            AdicionarEvento(pessoaEvent);
            await PublicarEventos();

            return request.ValidationResult;
        }
        private bool ValidarPessoa(Pessoa pessoa)
        {
            if (_pessoaRepository.Verificar(x => x.Cpf.Numero == pessoa.Cpf.Numero && x.Id != pessoa.Id))
            {
                AdicionarErro("Esse cpf já esta em uso");
                return false;
            }

            return true;
        }
    }
}
