using Cordillera.Distribuidas.Event.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajo_retiro.DTOs;
using multitrabajo_retiro.Messages.Commands;
using multitrabajo_retiro.Services;

namespace multitrabajo_retiro.Controllers
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

        [HttpPost("Withdrawal")]
        public async Task<ActionResult> Withdrawal(TransactionRequest request)
        {
            Models.Transaction transaction = new Models.Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Withdrawal"
            };
            transaction = await _transactionService.Withdrawal(transaction);
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
