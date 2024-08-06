using Cordillera.Distribuidas.Event.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajo_deposito.DTOs;
using multitrabajo_deposito.Messages.Commands;
using multitrabajo_deposito.Services;

namespace multitrabajo_deposito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceTransaction _transactionService;
        private readonly IServiceAccount _accountService;
        private readonly IEventBus _bus;
        public TransactionController(IServiceTransaction transactionService, IServiceAccount accountService, IEventBus bus)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _bus = bus;

        }

        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(TransactionRequest request)
        {
            Models.Transaction transaction = new Models.Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Deposit"
            };
            transaction = await _transactionService.Deposit(transaction);
            bool isProccess = _accountService.Execute(transaction);

            if (isProccess)
            {
                var transactionCreateCommand = new TransactionCreateCommand(
                   idTransaction: transaction.Id,
                   amount: transaction.Amount,
                   type: transaction.Type,
                   creationDate: transaction.CreationDate,
                   accountId: transaction.AccountId
                );

                await _bus.SendCommand(transactionCreateCommand);
            }

            return Ok(transaction);

        }
    }
}
