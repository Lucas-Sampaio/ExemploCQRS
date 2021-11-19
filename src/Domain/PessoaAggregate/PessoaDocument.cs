using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.PessoaAggregate
{
    //Classe que representa um documento no mongo
    public class PessoaDocument
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; }
        [BsonElement("Cpf")]
        public string Cpf { get; set; }
        [BsonElement("Endereco")]
        public List<EnderecoDocument> Enderecos { get; set; }
        [BsonElement("DataNascimento")]
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
    }
    public class EnderecoDocument
    {
        public int Id { get; set; }
        [BsonElement("CEP")]
        public string CEP { get; set; }
        [BsonElement("Numero")]
        public string Numero { get; set; }
        [BsonElement("Logradouro")]
        public string Logradouro { get; set; }
        [BsonElement("Bairro")]
        public string Bairro { get; set; }
        [BsonElement("Cidade")]
        public string Cidade { get; set; }
        [BsonElement("Estado")]
        public string Estado { get; set; }
    }
}
