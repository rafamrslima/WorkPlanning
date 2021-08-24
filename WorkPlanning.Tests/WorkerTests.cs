using System;
using WorkPlanning.Domain.Entities;
using Xunit;

namespace WorkPlanning.Tests
{
    public class WorkerTests
    {
        [Fact]
        public void CreateWorkerWhenDataIsValid()
        {
            //Arranje
            var name = "Rafael";
            var personalId = "54534565465";

            //Act
            var worker = new Worker(name, personalId);

            //Assert
            Assert.Equal("Rafael", worker.Name);
            Assert.NotNull(worker.Shifts);
        } 

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowsExceptionWhenNameIsNullOrEmpty(string name)
        {
            //Arranje 
            var personalId = "54534565465";

            //Act
            Action act = () => new Worker(name, personalId);

            //Assert
            Assert.Equal("Name can not be empty.", Assert.Throws<ArgumentException>(act).Message);
        }

        [Fact]
        public void ThrowsExceptionWhenNameIsShorterThan3Characters()
        {
            //Arranje
            var name = "AB";
            var personalId = "54534565465";

            //Act
            Action act = () => new Worker(name, personalId);

            //Assert
            Assert.Equal("Name should be at least 3 characters long.", Assert.Throws<ArgumentException>(act).Message);
        } 

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowsExceptionWhenPersonalIdIsNullOrEmpty(string personalId)
        {
            //Arranje
            var name = "Rafael"; 

            //Act
            Action act = () => new Worker(name, personalId);

            //Assert
            Assert.Equal("Personal Id can not be empty.", Assert.Throws<ArgumentException>(act).Message);
        }
         
    }
}

