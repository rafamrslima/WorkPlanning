using Moq;
using System;
using System.Threading.Tasks;
using WorkPlanning.Domain.Entities;
using WorkPlanning.Domain.Interfaces;
using WorkPlanning.Domain.Services;
using Xunit;

namespace WorkPlanning.Tests
{
    public class WorkerServiceTests
    {

        [Fact]
        public async Task ThrowsExceptionWhenPersonalIdIsDuplicated()
        {
            //Arranje
            var nameDb = "Rafael";
            var personalIdDb = "54534565465";
            var newWorker = new Worker("John", "54534565465");
            var workerRepositoryMock = new Mock<IWorkerRepository>();
            workerRepositoryMock.Setup(x => x.GetWorkerByPersonalId(personalIdDb)).ReturnsAsync(new Worker(nameDb, personalIdDb));

            var workerService = new WorkerService(workerRepositoryMock.Object);

            //Act  
            Func<Task> action = async () => await workerService.AddWorker(newWorker);

            //Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(action);
            Assert.Contains("Personal Id already registered.", ex.Message);
        }
    }
}
