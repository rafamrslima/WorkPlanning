using System.Threading.Tasks;
using WorkPlanning.Domain.Entities;

namespace WorkPlanning.Domain.Interfaces
{
    public interface IShiftService
    {
        Task AddShift(Shift shift);
        Task<Worker> GetShiftsByWorker(string id);
        Task RemoveShift(string shiftId, string workerId);
        Task<Worker> GetWorkerById(string id);
    }
}
