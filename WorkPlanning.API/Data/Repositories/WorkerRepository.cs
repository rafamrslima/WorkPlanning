using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkPlanning.API.Data.Context;
using WorkPlanning.API.Helpers;
using WorkPlanning.Domain.Entities;
using WorkPlanning.Domain.Interfaces;

namespace WorkPlanning.Data.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IMongoCollection<Worker> _workers;
        public WorkerRepository(IWorkPlanningDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _workers = database.GetCollection<Worker>(settings.WorkPlanningCollectionName);
        }

        public async Task AddWorker(Worker worker)
        {
            await _workers.InsertOneAsync(worker);
        }

        public async Task<List<Worker>> GetWorkers()
        {
            var fields = Builders<Worker>.Projection.Exclude(d => d.Shifts);
            return await _workers.Find(w => true).Project<Worker>(fields).ToListAsync();
        }

        public async Task<Worker> GetWorkerById(string id)
        {
            return await _workers.Find(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Worker> GetWorkerByPersonalId(string personalId)
        {
            return await _workers.Find(w => w.PersonalId == personalId).FirstOrDefaultAsync();
        }

        public async Task<List<Worker>> GetShiftsByDay(DateTime date)
        {
            var dateMaxTime = DateTimeHelper.GetDateMaxTime(date);

            var workers = await _workers.Find(w => w.Shifts.Any(s => s.StartTime >= date && s.StartTime <= dateMaxTime))
                .ToListAsync();

            foreach (var worker in workers)
                worker.Shifts = worker.Shifts.Where(x => x.StartTime.Date == date).ToList();

            return workers;
        }

        public async Task RemoveWorker(string id)
        {
            await _workers.DeleteOneAsync(w => w.Id == id);
        }

        public async Task AddShift(Shift shift, Worker worker)
        {
            worker.Shifts.Add(shift);
            await _workers.ReplaceOneAsync(w => w.Id == worker.Id, worker);
        }

        public async Task RemoveShift(Worker worker, string shiftId)
        {
            worker.Shifts.RemoveAll(s => s.Id == shiftId);
            await _workers.ReplaceOneAsync(w => w.Id == worker.Id, worker);
        }
    }
}
