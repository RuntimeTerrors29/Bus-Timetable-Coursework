using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.models;

namespace BusTimetable.Tests
{
    [TestClass]
    public class ScheduleTests
    {
        // Test the Schedule class constructor and properties

        [TestMethod]
        public void AvailableSeats_ReturnsCapacityMinusBooked()
        {
            var schedule = new Schedule(1, 1, "Route 1", TimeSpan.Zero, TimeSpan.Zero, 50, 10);
            Assert.AreEqual(40, schedule.AvailableSeats);


        }
        // Test the IsFull property 
        [TestMethod]
        public void IsFull_ReturnsTrueWhenNoSeatsAvailable()
        {
            var schedule = new Schedule(1, 1, "Route 1", TimeSpan.Zero, TimeSpan.Zero, 50, 50);
            Assert.IsTrue(schedule.IsFull);

        }
        [TestMethod]

        public void IsFull_ReturnsFalseWhenSeatsAvailable()
        {
            var schedule = new Schedule(1, 1, "Route 1", TimeSpan.Zero, TimeSpan.Zero, 50, 0);
            Assert.IsFalse(schedule.IsFull);


        }
        // Test the ToString method

        [TestMethod]
        public void ToString_ContainsRouteName()
        {
            var schedule = new Schedule(1, 1, "Victoria to Bank", new TimeSpan(8, 0, 0), new TimeSpan(8, 30, 0), 50, 0);
            Assert.IsTrue(schedule.ToString().Contains("Victoria to Bank"));

        }
    }

}