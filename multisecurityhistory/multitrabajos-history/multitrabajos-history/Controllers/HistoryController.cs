using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajos_history.DTOs;
using multitrabajos_history.Repositories;

namespace multitrabajos_history.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IServicesHistory _historyService;
        public HistoryController(IServicesHistory historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(int accountId)
        {
            IEnumerable<HistoryResponse> model = null;

            var data = await _historyService.GetAll();
            model = data.Where(x => x.AccountId == accountId).ToList();

            return Ok(model);
        }
    }
}
