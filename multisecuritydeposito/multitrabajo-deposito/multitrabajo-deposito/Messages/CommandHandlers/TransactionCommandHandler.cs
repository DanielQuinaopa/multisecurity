﻿using Cordillera.Distribuidas.Event.Bus;
using MediatR;
using multitrabajo_deposito.Messages.Commands;
using multitrabajo_deposito.Messages.Events;

namespace multitrabajo_deposito.Messages.CommandHandlers
{
    public class TransactionCommandHandler : IRequestHandler<TransactionCreateCommand, bool>
    {
            private readonly IEventBus _bus;

            public TransactionCommandHandler(IEventBus bus)
            {
                _bus = bus;
            }

            public Task<bool> Handle(TransactionCreateCommand request, CancellationToken cancellationToken)
            {
                _bus.Publish(new TransactionCreatedEvent(
                       request.IdTransaction,
                       request.Amount,
                       request.Type,
                       request.CreationDate,
                       request.AccountId
                   ));
                return Task.FromResult(true);

            }

    }
}
