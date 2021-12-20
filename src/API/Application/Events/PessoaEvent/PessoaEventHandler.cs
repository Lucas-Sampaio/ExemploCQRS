using MediatR;
using MessageBus;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Events.PessoaEvent
{
    public class PessoaEventHandler :
        INotificationHandler<PessoaCadastrataEvent>
    {
        private readonly IMessageBus _bus;

        public PessoaEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(PessoaCadastrataEvent notification, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new PessoaCadastrataEvent(notification.PessoaId));
        }
    }
}
