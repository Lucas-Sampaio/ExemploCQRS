namespace Core.Messages.Integration
{
    public class PessoaCadastradaIntegrationEvent : IntegrationEvent
    {
        public PessoaCadastradaIntegrationEvent(int id)
        {
            PessoaId = id;
        }
        public int PessoaId { get; set; }
    }
}
