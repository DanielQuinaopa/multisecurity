using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajos_cuenta.DTOs;
using multitrabajos_cuenta.Services;

namespace multitrabajos_cuenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServiceAccount _accountService;
        public AccountController(IServiceAccount accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _accountService.GetAll());
        }
        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(AccountRequest request)
        {
            var result = await _accountService.GetbyId(request.IdAccount);
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount + request.Amount,
                Customer = result.Customer
            };
            await _accountService.Deposit(account);
            return Ok();
        }
        [HttpPost("Withdrawal")]
        public async Task<ActionResult> Withdrawal(AccountRequest request)
        {
            var result = await _accountService.GetbyId(request.IdAccount);
            if (result.TotalAmount < request.Amount)
            {
                return BadRequest(new { message = "The indicated amount cannot be withdrawal" });
            }
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount - request.Amount,
                Customer = result.Customer
            };
            await _accountService.Withdrawal(account);
            return Ok();
        }
    }
}
