using System;
using System.Linq;
using WorkPlanning.Domain.Entities;
using Xunit;

namespace WorkPlanning.Tests
{
    public class ShiftTests
    {
        [Fact]
        public void CreateShiftWhenDataIsValid()
        {
            //Arranje
            var worker = new Worker("Rafael", "3847384878");
            var shiftTime = new DateTime(2021, 10, 11).AddHours(8);

            //Act
            var shift = new Shift(shiftTime, worker.Id);

            //Assert
            Assert.NotNull(shift);
            Assert.Equal(shift.StartTime, shiftTime);
        }

        [Fact]
        public void ShiftShouldBe8HoursLongWhenShiftIsCreated()
        {
            //Arranje
            var worker = new Worker("Rafael", "3847384878");

            //Act
            var shift = new Shift(new DateTime(2021, 10, 11).AddHours(8), worker.Id);
            worker.Shifts.Add(shift);

            //Assert
            Assert.Equal(new DateTime(2021, 10, 11), worker.Shifts.FirstOrDefault().EndTime.Date);
            Assert.Equal(16, worker.Shifts.FirstOrDefault().EndTime.Hour);
        }

        [Fact]
        public void ThrowsExceptionWhenShiftHasStartDateUnassigned()
        {
            //Arranje
            var worker = new Worker("Rafael", "3847384878");
            var shiftStartTime = new DateTime();

            //Act
            Action act = () => new Shift(shiftStartTime, worker.Id);

            //Assert
            Assert.Equal("Start time should be defined.", Assert.Throws<ArgumentException>(act).Message);
        } 
    }
}
