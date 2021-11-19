namespace API.Application.DTOs
{
    //objeto de resposta
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
