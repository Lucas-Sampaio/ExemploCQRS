using Domain.PessoaAggregate;
using Domain.SeedWork;
using Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        public PessoaRepository(ProjetoContext context)
        {
            _context = context;
        }
        private readonly ProjetoContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public Pessoa ObterPorId(int id, params string[] props)
        {
            return _context.Pessoas.DynamicInclude(props).SingleOrDefault(x => x.Id == id);
        }

        public Pessoa Adicionar(Pessoa pessoa)
        {
            var entidade = _context.Pessoas.Add(pessoa).Entity;
            return entidade;
        }

        public Pessoa Atualizar(Pessoa pessoa)
        {
            return _context.Update(pessoa).Entity;
        }

        public bool Verificar(Expression<Func<Pessoa, bool>> expression)
        {
            return _context.Pessoas.Any(expression);
        }

        public void RemoverEndereco(int enderecoID)
        {
            var endereco = _context.Enderecos.Find(enderecoID);
            _context.Enderecos.Remove(endereco);
        }
    }
}
