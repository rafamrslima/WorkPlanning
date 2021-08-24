using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlanning.Domain.Entities;
using WorkPlanning.Domain.Interfaces;
using WorkPlanning.Domain.Services;
using Xunit;

namespace WorkPlanning.Tests
{
    public class ShiftServiceTests
    {
        [Fact]
        public async Task ThrowsExceptionWhenWorkerIdIsNotFound()
        {
            //Arranje 
            var shift = new Shift(new DateTime(2021, 10, 11).AddHours(8), "#12345");
            var workerRepositoryMock = new Mock<IWorkerRepository>();
            var shiftService = new ShiftService(workerRepositoryMock.Object);

            //Act  
            Func<Task> action = async () => await shiftService.AddShift(shift);

            //Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(action);
            Assert.Contains("Worker not found.", ex.Message);
        }

        [Fact]
        public async Task ThrowsExceptionWhenWorkerHasShiftOnTheSameDay()
        {
            //Arranje
            var worker = new Worker("Rafael", "347343") { Id = "#12345" };
            var shift = new Shift(new DateTime(2021, 10, 11).AddHours(8), worker.Id);
            var shift2 = new Shift(new DateTime(2021, 10, 11).AddHours(16), worker.Id);
            worker.Shifts.Add(shift);
            var workerRepositoryMock = new Mock<IWorkerRepository>();
            workerRepositoryMock.Setup(x => x.GetWorkerById(worker.Id)).ReturnsAsync(worker);

            var shiftService = new ShiftService(workerRepositoryMock.Object);

            //Act  
            Func<Task> action = async () => await shiftService.AddShift(shift2);

            //Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(action);
            Assert.Contains("Worker already has shift on this day.", ex.Message);
        }
    }
}
