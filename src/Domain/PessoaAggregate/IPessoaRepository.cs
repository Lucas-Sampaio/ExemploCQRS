using Domain.SeedWork;
using System;
using System.Linq.Expressions;

namespace Domain.PessoaAggregate
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Pessoa Adicionar(Pessoa pessoa);
        Pessoa Atualizar(Pessoa pessoa);
        /// <summary>
        /// Faz uma consulta no banco e retorna true se satisfazer a condição
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Verificar(Expression<Func<Pessoa, bool>> expression);
    }
}
