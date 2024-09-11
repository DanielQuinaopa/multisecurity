using Cordillera.Distribuidas.Event.Bus;
using MediatR;
using multitrabajo_retiro.Messages.Commands;
using multitrabajo_retiro.Messages.Events;


namespace multitrabajo_retiro.Messages.CommandHandlers
{
    public class UsuerCommandHandler : IRequestHandler<UsuerCreateCommand, bool>
    {
        private readonly IEventBus _bus;

        public UsuerCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(UsuerCreateCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new UsuerCreatedEvent(
                            request.IdCustomer,
                            request.FullName,
                            request.Email
               ));
            return Task.FromResult(true);

        }
    }
}
