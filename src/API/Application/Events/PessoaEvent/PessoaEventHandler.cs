using AutoMapper;
using Domain.PessoaAggregate;
using Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Events.PessoaEvent
{
    public class PessoaEventHandler :
        INotificationHandler<PessoaAdicionadaEvent>,
        INotificationHandler<PessoaAtualizadaEvent>
    {
        private readonly PessoaMongoRepository _pessoaRepository;
        private readonly IPessoaRepository _pessoaEfRepository;
        private readonly IMapper _mapper;
        public PessoaEventHandler(PessoaMongoRepository pessoaRepository, IMapper mapper, IPessoaRepository pessoaEfRepository)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
            _pessoaEfRepository = pessoaEfRepository;
        }
        public Task Handle(PessoaAdicionadaEvent notification, CancellationToken cancellationToken)
        {
            var pessoaDocument = _mapper.Map<PessoaDocument>(notification);
            //verifica se não existe e adiciona, 
            //esta chamando o evento 2x n sei o motivo ainda
            var pessoa = _pessoaRepository.ObterPorId(pessoaDocument.Id);
            if (pessoa == null)
                _pessoaRepository.Adicionar(pessoaDocument);
            return Task.CompletedTask;
        }

        public Task Handle(PessoaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            var pessoa = _pessoaEfRepository.ObterPorId(notification.Id, "Enderecos");
            var pessoaDocument = _mapper.Map<PessoaDocument>(pessoa);
            //verifica se não existe e adiciona, 
            //esta chamando o evento 2x n sei o motivo ainda
            _pessoaRepository.Atualizar(pessoaDocument.Id, pessoaDocument);
            return Task.CompletedTask;
        }
    }
}
