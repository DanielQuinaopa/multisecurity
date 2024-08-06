using multitrabajo_deposito.DTOs;
using multitrabajo_deposito.Models;
using System.Security.Principal;

namespace multitrabajo_deposito.Services
{
    public class ServiceAccount : IServiceAccount
    {
        //Consumir la url del otro microservicio appsetting.json
        private readonly IConfiguration _configuration;
        private readonly IServiceTransaction _transactionService;
        private readonly IHttpClient _httpClient;
        public ServiceAccount(IConfiguration configuration, IServiceTransaction transactionService, IHttpClient httpClient)
        {
            _configuration = configuration;
            _transactionService = transactionService;
            _httpClient = httpClient;
        }
        public async Task<bool> DepositAccount(AccountRequest request)
        {
            string uri = _configuration["proxy:urlAccountDeposit"];
            var response = await _httpClient.PostAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }
        private async Task<bool> NotifyTransaction(NotifyRequest request)
        {
            string uri = _configuration["proxy:urlNotifyDeposit"];
            var response = await _httpClient.PostAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public bool Execute(Transaction request)
        {
            bool response = false;
            AccountRequest account = new AccountRequest()
            {
                Amount = request.Amount,
                IdAccount = request.AccountId
            };
            response = DepositAccount(account).Result;
            // Notificar transacción
            NotifyRequest notifyRequest = new NotifyRequest
            {
                IdCuenta = request.AccountId,
                Tipo = request.Type,
                Valor = request.Amount
            };
            NotifyTransaction(notifyRequest).Wait();

            return response;
        }

    }
}
