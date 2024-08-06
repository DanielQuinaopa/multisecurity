using multitrabajo_retiro.DTOs;
using multitrabajo_retiro.Models;

namespace multitrabajo_retiro.Services
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
        public async Task<bool> WithdrawAccount(AccountRequest request)
        {
            // Asumiendo que hay una URL específica para el retiro
            string uri = _configuration["proxy:urlAccountWithdrawal"];
            var response = await _httpClient.PostAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }
        private async Task<bool> NotifyTransaction(NotifyRequest request)
        {
            string uri = _configuration["proxy:urlNotifyWithdrawal"];
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
            response = WithdrawAccount(account).Result;
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
