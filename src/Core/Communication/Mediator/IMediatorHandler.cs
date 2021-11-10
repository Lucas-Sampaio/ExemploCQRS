using Core.Messages;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
