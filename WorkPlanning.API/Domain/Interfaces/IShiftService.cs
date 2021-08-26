using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Domain.Entities;

namespace WorkPlanning.Domain.Interfaces
{
    public interface IShiftService
    {
        Task AddShift(Shift shift);
        Task<Worker> GetShiftsByWorker(string id);
        Task<List<Worker>> GetShiftsByDay(DateTime date);
        Task RemoveShift(string shiftId, string workerId);
        Task<Worker> GetWorkerById(string id);
    }
}
