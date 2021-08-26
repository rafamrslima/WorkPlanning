using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkPlanning.API.DTOs;
using WorkPlanning.API.Helpers;
using WorkPlanning.Domain.Entities;
using WorkPlanning.Domain.Interfaces;

namespace WorkPlanning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        readonly private IShiftService _shiftService;
        public ShiftsController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpPost()]
        public async Task<IActionResult> AddShift([FromBody] ShiftDto shiftDto)
        {
            var shift = new Shift(shiftDto.StartTime, shiftDto.WorkerId);
            await _shiftService.AddShift(shift);
            return Ok("Created");
        }

        [HttpGet("worker")]
        public async Task<IActionResult> GetShiftsByWorker([FromQuery]string id)
        {
            return Ok(await _shiftService.GetShiftsByWorker(id));
        }

        [HttpGet()]
        public async Task<IActionResult> GetShiftsByDay([FromQuery]string date)
        {
            var dateFilter = DateTimeHelper.StringToDate(date);
            return Ok(await _shiftService.GetShiftsByDay(dateFilter));
        }

        [HttpDelete("{workerId}/{shiftId}")]
        public async Task<IActionResult> RemoveShift(string workerId, string shiftId)
        {
            await _shiftService.RemoveShift(workerId, shiftId);
            return Ok("Deleted");
        }
    }
}
