using Domain.PessoaAggregate;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PessoaMongoRepository : IPessoaMongoRepository
    {
        private readonly IMongoCollection<PessoaDocument> _pessoas;
        private const string _pessoaCollection = "Pessoas";
        public PessoaMongoRepository(IMongoDBContext mongoContext)
        {
            _pessoas = mongoContext.GetCollection<PessoaDocument>(_pessoaCollection);
        }
        public List<PessoaDocument> ObterTodos()
        {
            return _pessoas.Find(_ => true).ToList();
        }
        public PessoaDocument ObterPorId(int id)
        {
            return _pessoas.Find(pessoa => pessoa.Id == id).SingleOrDefault();
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
        public void Remover(int id)
        {
            var filter = Builders<PessoaDocument>.Filter.Where(_ => _.Id == id);
            var operation = _pessoas.DeleteOne(filter);
        }
    }
}
