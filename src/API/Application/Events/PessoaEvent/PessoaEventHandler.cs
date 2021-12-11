using AutoMapper;
using Domain.PessoaAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Events.PessoaEvent
{
    public class PessoaEventHandler :
        INotificationHandler<PessoaAdicionadaEvent>,
        INotificationHandler<PessoaAtualizadaEvent>
    {
        private readonly IPessoaMongoRepository _pessoaRepository;
        private readonly IPessoaRepository _pessoaEfRepository;
        private readonly IMapper _mapper;
        public PessoaEventHandler(IPessoaMongoRepository pessoaRepository, IMapper mapper, IPessoaRepository pessoaEfRepository)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
            _pessoaEfRepository = pessoaEfRepository;
        }
        public Task Handle(PessoaAdicionadaEvent notification, CancellationToken cancellationToken)
        {
            //verifica se não existe e adiciona, 
            //esta chamando o evento 2x n sei o motivo ainda
            var pessoaDocument = _mapper.Map<PessoaDocument>(notification);
            _pessoaRepository.AdicionarOuAtualizarPessoa(pessoaDocument);
            return Task.CompletedTask;
        }

        public Task Handle(PessoaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            var pessoa = _pessoaEfRepository.ObterPorId(notification.Id, "Enderecos");
            var pessoaDocument = _mapper.Map<PessoaDocument>(pessoa);
            //verifica se não existe e adiciona, 
            //esta chamando o evento 2x n sei o motivo ainda
            _pessoaRepository.AdicionarOuAtualizarPessoa(pessoaDocument);
            return Task.CompletedTask;
        }
    }
}
