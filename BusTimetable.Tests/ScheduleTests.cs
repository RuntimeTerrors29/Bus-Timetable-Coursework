using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;

namespace BusTimetable.Tests
{
    [TestClass]
    public class ScheduleTests
    {
        [TestMethod]
        // 50 capacity, 20 booked = 30 available
        public void Test_AvailableSeats_Correct()
        {
            var schedule = new Schedule(1, 101, "Route 1",
                TimeSpan.Parse("08:00"), TimeSpan.Parse("08:30"), 50, 20);
            Assert.AreEqual(30, schedule.AvailableSeats);
        }

        [TestMethod]
        public void Test_IsFull_True()
        {
            var schedule = new Schedule(2, 102, "Route 2",
                TimeSpan.Parse("09:00"), TimeSpan.Parse("09:30"), 50, 50);
            Assert.IsTrue(schedule.IsFull);
        }

        [TestMethod]
        public void Test_IsFull_False()
        {
            var schedule = new Schedule(3, 103, "Route 3",
                TimeSpan.Parse("10:00"), TimeSpan.Parse("10:30"), 50, 30);
            Assert.IsFalse(schedule.IsFull);
        }

        [TestMethod]
        public void Test_ScheduleID_Correct()
        {
            var schedule = new Schedule(4, 104, "Route 4",
                TimeSpan.Parse("11:00"), TimeSpan.Parse("11:30"), 50);
            Assert.AreEqual(4, schedule.ScheduleID);
        }

        [TestMethod]
        public void Test_Capacity_Correct()
        {
            var schedule = new Schedule(5, 105, "Route 5",
                TimeSpan.Parse("12:00"), TimeSpan.Parse("12:30"), 50);
            Assert.AreEqual(50, schedule.Capacity);
        }

        [TestMethod]
        public void Test_SeatsBooked_Correct()
        {
            var schedule = new Schedule(6, 106, "Route 6",
                TimeSpan.Parse("13:00"), TimeSpan.Parse("13:30"), 50, 25);
            Assert.AreEqual(25, schedule.SeatsBooked);
        }
    }
}
