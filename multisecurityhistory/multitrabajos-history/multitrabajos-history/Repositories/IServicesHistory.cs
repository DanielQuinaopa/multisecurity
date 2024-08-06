using multitrabajos_history.DTOs;
using multitrabajos_history.Models;
using System.Data.SqlTypes;

namespace multitrabajos_history.Repositories
{
    public interface IServicesHistory
    {
        Task<IEnumerable<HistoryResponse>> GetAll();
        Task<bool> Add(HistoryTransaction historyTransaction);
    }
}
