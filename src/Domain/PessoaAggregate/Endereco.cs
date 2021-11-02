using Domain.SeedWork;

namespace Domain.PessoaAggregate
{
    public class Endereco : Entity
    {
        //EF
        protected Endereco() { }
       
        public Endereco(string logradouro, string numero, string cep,string bairro ,string cidade, string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            CEP = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
