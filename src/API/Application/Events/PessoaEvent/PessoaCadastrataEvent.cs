using Core.Messages.Integration;

namespace API.Application.Events.PessoaEvent
{
    public class PessoaCadastrataEvent : IntegrationEvent
    {
        public PessoaCadastrataEvent(int id)
        {
            PessoaId = id;
        }
        public int PessoaId { get; set; }
    }
}
