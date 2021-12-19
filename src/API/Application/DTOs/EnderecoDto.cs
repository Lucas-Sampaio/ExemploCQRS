namespace API.Application.DTOs
{
    //objeto de resposta
    public class EnderecoDto
    {
        public EnderecoDto() { }

        public EnderecoDto(string logradouro, string numero, string cep, string bairro, string cidade, string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            CEP = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public int Id { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
