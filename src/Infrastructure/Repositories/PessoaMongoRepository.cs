using Domain.PessoaAggregate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class PessoaMongoRepository
    {
        private readonly IMongoCollection<PessoaDocument> _pessoas;
        private const string _pessoaDB = "PessoaDB";
        private const string _pessoaCollection = "Pessoas";
        public PessoaMongoRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "String de conexão não pode ser vazia");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(_pessoaDB);
            _pessoas = database.GetCollection<PessoaDocument>(_pessoaCollection);
        }
        public List<PessoaDocument> ObterTodos()
        {
            return _pessoas.Find(_ => true).ToList();
        }
        public PessoaDocument ObterPorId(int id)
        {
            return _pessoas.Find(pessoa => pessoa.Id == id).SingleOrDefault();
        }
        public void teste(Expression<Func<PessoaDocument, bool>> predicate)
        {
            var x = _pessoas.AsQueryable().Where(predicate.Compile()).ToList();
             
        }
        public void Adicionar(PessoaDocument pessoa)
        {
            _pessoas.InsertOne(pessoa);
        }
        public void Atualizar(int id, PessoaDocument pessoa)
        {
            var filter = Builders<PessoaDocument>.Filter.Where(_ => _.Id == id);
            _pessoas.ReplaceOne(filter, pessoa);
        }
        public void Remove(long id)
        {
            var filter = Builders<PessoaDocument>.Filter.Where(_ => _.Id == id);
            var operation = _pessoas.DeleteOne(filter);
        }
    }
}
