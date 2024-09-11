using Cordillera.Distribuidas.Event.Bus;
using multitrabajos_cuenta.Models;
using multitrabajos_cuenta.Services;
using multitrabajos_cuenta.Messages.Events;

namespace multitrabajos_history.Messages.EventsHandlers
{
    public class UsuerEventHandler : IEventHandler<UsuerCreatedEvent>
    {
        private readonly IServiceAccount _accountService;

        public UsuerEventHandler(IServiceAccount accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(UsuerCreatedEvent @event)
        {
            try
            {
                var customer = new Customer
                {
                    IdCustomer = @event.IdCustomer,
                    FullName = @event.FullName,
                    Email = @event.Email,
                };
                _ = await _accountService.NewAccount(customer);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
