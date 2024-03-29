﻿using Core.Communication.Mediator;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Messages
{
    public class CommandHandler
    {
        protected ValidationResult ValidationResult;
        protected readonly IMediatorHandler MediatorHandler;
        protected CommandHandler(IMediatorHandler mediatorHandler = null)
        {
            MediatorHandler = mediatorHandler;
            ValidationResult = new ValidationResult();
        }
        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        public void AdicionarEvento(Event evento)
        {
            _notificacoes ??= new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event eventItem)
        {
            _notificacoes?.Remove(eventItem);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }
        public async Task PublicarEventos()
        {
            if (MediatorHandler == null)
                throw new ArgumentNullException(nameof(MediatorHandler), "mediatorHandler não pode ser nulo");
          
            var tasks = Notificacoes
                .Select(async (domainEvent) =>
                {
                    Console.WriteLine("Entrou no evento");
                    await MediatorHandler.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
            LimparEventos();
        }
    }
}
