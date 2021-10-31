using Domain.SendWork;

namespace Domain.PessoaAggregate
{
    public class Endereco : Entity
    {
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
