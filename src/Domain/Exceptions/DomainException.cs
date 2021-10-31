using System;

namespace Domain.Exceptions
{
    /// <summary>
    /// Representa as excenções de dominio
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }

    }
}
