using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WorkPlanning.API.DTOs;
using WorkPlanning.Domain.Entities;
using WorkPlanning.Domain.Interfaces;

namespace WorkPlanning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        readonly private IWorkerService _workerService;
        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWorker(WorkerDto workerDto)
        {
            var worker = new Worker(workerDto.Name, workerDto.PersonalId);
            await _workerService.AddWorker(worker);
            return Ok("Created");
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkers()
        {
            var result = await _workerService.GetWorkers();
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveWorker(string id)
        {
            await _workerService.RemoveWorker(id);
            return Ok("Deleted");
        }
    }
}
