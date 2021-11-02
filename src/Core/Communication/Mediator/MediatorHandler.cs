using Core.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;

namespace Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }
    }
}
