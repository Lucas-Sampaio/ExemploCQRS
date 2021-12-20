using Core.Messages.Integration;
using MediatR;
using MessageBus;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Events.PessoaEvent
{
    public class PessoaEventHandler :
        INotificationHandler<PessoaCadastradaIntegrationEvent>
    {
        private readonly IMessageBus _bus;

        public PessoaEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(PessoaCadastradaIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(notification);
        }
    }
}
