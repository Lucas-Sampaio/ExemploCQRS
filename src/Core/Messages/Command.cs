using FluentValidation.Results;
using MediatR;
using System;

namespace Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
        //estou colocando como validação obrigatoria dos comandos
        //mas pode ser opcional é so remover abstract e deixar
        //public virtual bool EhValido(){}
        public abstract bool EhValido();
      
    }
}
