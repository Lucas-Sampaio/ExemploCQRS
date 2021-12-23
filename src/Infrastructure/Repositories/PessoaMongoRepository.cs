using Domain.PessoaAggregate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<PessoaDocument>> ObterTodosAsync()
        {
            return await _pessoas.Find(_ => true).ToListAsync();
        }
        public async Task<PessoaDocument> ObterPorIdAsync(int id)
        {
            return await _pessoas.Find(pessoa => pessoa.Idtest == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Adiciona uma pessoa caso não esteja no mongo ou atualiza ela caso tenha
        /// </summary>
        /// <param name="pessoa">pessoa a ser inserida/atualizada</param>
        /// <param name="id">id da pessoa</param>
        public async Task AdicionarOuAtualizarAsync(PessoaDocument pessoa)
        {
            var id = pessoa.Idtest;
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id da pessoa não informada");

            var pessoaDocument = await ObterPorIdAsync(id);
            if (pessoaDocument == null)
               await AdicionarAsync(pessoa);
            else
               await AtualizarAsync(id, pessoa);
        }
        private Task AdicionarAsync(PessoaDocument pessoa) => _pessoas.InsertOneAsync(pessoa);
        private Task AtualizarAsync(int id, PessoaDocument pessoa)
        {
            var filter = Builders<PessoaDocument>.Filter.Where(_ => _.Idtest == id);
            return _pessoas.ReplaceOneAsync(filter, pessoa);
        }
        public async Task RemoverAsync(int id)
        {
            var filter = Builders<PessoaDocument>.Filter.Where(_ => _.Idtest == id);
           _= await _pessoas.DeleteOneAsync(filter);
        }

        public PessoaDocument ObterPorCPF(string cpf)
        {
            return _pessoas.Find(pessoa => pessoa.Cpf == cpf).FirstOrDefault();
        }
    }
}
