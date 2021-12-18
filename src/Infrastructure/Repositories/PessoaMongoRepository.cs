using Domain.PessoaAggregate;
using MongoDB.Driver;
using System;
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
        /// <summary>
        /// Adiciona uma pessoa caso não esteja no mongo ou atualiza ela caso tenha
        /// </summary>
        /// <param name="pessoa">pessoa a ser inserida/atualizada</param>
        /// <param name="id">id da pessoa</param>
        public void AdicionarOuAtualizarPessoa(PessoaDocument pessoa)
        {
            var id = pessoa.Id;
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id da pessoa não informada");

            var pessoaDocument = ObterPorId(id);
            if (pessoaDocument == null)
                Adicionar(pessoa);
            else
                Atualizar(id, pessoa);
        }
        private void Adicionar(PessoaDocument pessoa) => _pessoas.InsertOne(pessoa);
        private void Atualizar(int id, PessoaDocument pessoa)
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
