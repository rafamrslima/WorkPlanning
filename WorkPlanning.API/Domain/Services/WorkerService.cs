using System.Collections.Generic;
using WorkPlanning.Domain.Interfaces;
using WorkPlanning.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace WorkPlanning.Domain.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        public WorkerService(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }
        public async Task AddWorker(Worker worker)
        {
            var workerDb = await _workerRepository.GetWorkerByPersonalId(worker.PersonalId);
            if (workerDb != null)
                throw new InvalidOperationException("Personal Id already registered.");

            await _workerRepository.AddWorker(worker);
        }

        public async Task<List<Worker>> GetWorkers()
        {
            return await _workerRepository.GetWorkers();
        }
         
        public async Task RemoveWorker(string id)
        {
            var worker = await _workerRepository.GetWorkerById(id);
            if (worker == null)
                throw new KeyNotFoundException("Worker not found.");

            await _workerRepository.RemoveWorker(id);
        }
    }
}
