using System;
using System.Threading.Tasks;

namespace Domain.SendWork
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
