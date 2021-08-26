using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkPlanning.Domain.Entities;
using WorkPlanning.Domain.Interfaces;

namespace WorkPlanning.Domain.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IWorkerRepository _workerRepository;
        public ShiftService(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task AddShift(Shift shift)
        {
            var worker = await GetWorkerById(shift.WorkerId);

            if (worker == null)
                throw new KeyNotFoundException("Worker not found.");

            if (worker.Shifts.Any(x => x.StartTime.Date == shift.StartTime.Date))
                throw new InvalidOperationException("Worker already has shift on this day.");

            await _workerRepository.AddShift(shift, worker);
        }

        public async Task<Worker> GetShiftsByWorker(string id)
        {
            var worker = await GetWorkerById(id);
            if (worker == null)
                throw new KeyNotFoundException("Worker not found.");

            return worker;
        }

        public async Task<List<Worker>> GetShiftsByDay(DateTime date) => await _workerRepository.GetShiftsByDay(date);

        public async Task RemoveShift(string workerId, string shiftId)
        {
            var workerDb = await GetWorkerById(workerId);

            if (workerDb == null)
                throw new KeyNotFoundException("Worker not found.");

            if (!workerDb.Shifts.Any(w => w.Id == shiftId))
                throw new KeyNotFoundException("Shift not found.");

            await _workerRepository.RemoveShift(workerDb, shiftId);
        }

        public async Task<Worker> GetWorkerById(string id)  => await _workerRepository.GetWorkerById(id);
    }
}
