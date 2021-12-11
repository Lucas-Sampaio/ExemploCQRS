using Domain.SeedWork;
using System;
using System.Linq.Expressions;

namespace Domain.PessoaAggregate
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        /// <summary>
        /// Obtem uma pessoa pelo id
        /// </summary>
        /// <param name="id">Id da pessoa</param>
        /// <param name="props">Nomes das propriedades de navegação a serem buscadas</param>
        /// <returns></returns>
        Pessoa ObterPorId(int id, params string[] props);
        void Adicionar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        /// <summary>
        /// Faz uma consulta no banco e retorna true se satisfazer a condição
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Verificar(Expression<Func<Pessoa, bool>> expression);
        void RemoverEndereco(int enderecoID);
    }
}
