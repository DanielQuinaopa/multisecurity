using Cordillera.Distribuidas.Event.Bus;
using MediatR;
using multitrabajo_retiro.Messages.Commands;
using multitrabajo_retiro.Messages.Events;

namespace multitrabajo_retiro.Messages.CommandHandlers
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
