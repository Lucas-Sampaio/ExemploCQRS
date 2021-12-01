using Core.Messages;

namespace API.Application.Events.PessoaEvent
{
    public class PessoaAtualizadaEvent : Event
    {
        public PessoaAtualizadaEvent(int pessoaID)
        {
            Id = pessoaID;
        }
        public int Id { get; set; }
    }
}
