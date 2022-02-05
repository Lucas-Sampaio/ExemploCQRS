using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Domain.PessoaAggregate
{
    /// <summary>
    /// Classe pessoa que servirá para ser salva no cosmo db e recuperada
    /// </summary>
    public class PessoaCosmo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "partition")]
        public string Partition { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public EnderecoCosmo[] Enderecos { get; set; }
    }
    public class EnderecoCosmo
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
