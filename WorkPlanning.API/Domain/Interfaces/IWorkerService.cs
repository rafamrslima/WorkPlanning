using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Domain.Entities;

namespace WorkPlanning.Domain.Interfaces
{
    public interface IWorkerService
    {
        Task AddWorker(Worker worker);
        Task<List<Worker>> GetWorkers();
        Task RemoveWorker(string id);
    }
}
