using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Domain.Entities;

namespace WorkPlanning.Domain.Interfaces
{
    public interface IWorkerRepository
    {
        Task AddWorker(Worker worker);
        Task<List<Worker>> GetWorkers();
        Task<Worker> GetWorkerById(string id);
        Task RemoveWorker(string id);
        Task AddShift(Shift shift, Worker worker);
        Task RemoveShift(Worker shiftId, string workerId);
        Task<Worker> GetWorkerByPersonalId(string personalId);
    }
}
