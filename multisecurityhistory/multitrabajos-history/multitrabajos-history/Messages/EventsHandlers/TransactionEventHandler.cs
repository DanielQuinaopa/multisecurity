﻿using Cordillera.Distribuidas.Event.Bus;
using multitrabajos_history.Messages.Events;
using multitrabajos_history.Models;
using multitrabajos_history.Repositories;
using multitrabajos_history.Services;

namespace multitrabajos_history.Messages.EventsHandlers
{
    public class TransactionEventHandler : IEventHandler<TransactionCreatedEvent>
    {
        private readonly IServiceHistory _historyService;

        public TransactionEventHandler(IServiceHistory historyService)
        {
            _historyService = historyService;
        }

        public Task Handle(TransactionCreatedEvent @event)
        {
            try
            {
                _historyService.Add(new HistoryTransaction()
                {
                    IdTransaction = @event.IdTransaction,
                    Amount = @event.Amount,
                    Type = @event.Type,
                    CreationDate = @event.CreationDate,
                    AccountId = @event.AccountId

                });
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.CompletedTask;
        }
    }
}
