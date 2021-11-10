using Core.Messages;
using System;

namespace API.Application.Events.PessoaEvent
{
    public class PessoaAdicionadaEvent : Event
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
