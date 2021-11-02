using System;
using System.Threading.Tasks;

namespace Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Confirma as transações
        /// </summary>
        /// <returns></returns>
        Task<bool> Commit();
    }
}
