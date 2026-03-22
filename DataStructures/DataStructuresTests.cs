using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;
using BusTimetable.DataStructures;

namespace BusTimetable.Tests
{
    [TestClass]
    public class DataStructuresTests
    {

        [TestMethod]
        public void HashTable_Add_MultipleStops_AllRetrievable()
        {
            var table = new BusStopHashTable(8);
            for (int i = 1; i <= 10; i++)
                table.Add(new BusStop(i, $"Stop {i}", "Location", 0, 0));
            Assert.AreEqual(10, table.Count);
            for (int i = 1; i <= 10; i++)
                Assert.IsNotNull(table.GetById(i));
        }

        [TestMethod]
        public void HashTable_GetAll_ReturnsAllStops()
        {
            var table = new BusStopHashTable();
            table.Add(new BusStop(1, "A", "Loc", 0, 0));
            table.Add(new BusStop(2, "B", "Loc", 0, 0));
            table.Add(new BusStop(3, "C", "Loc", 0, 0));
            Assert.AreEqual(3, table.GetAll().Length);
        }

        [TestMethod]
        public void HashTable_Remove_NonExistentId_ReturnsFalse()
        {
            Assert.IsFalse(new BusStopHashTable().Remove(999));
        }

        [TestMethod]
        public void TimetableList_InsertSorted_MaintainsDepartureOrder()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(9, 0, 0)));
            list.InsertSorted(MakeSched(2, new TimeSpan(7, 0, 0)));
            list.InsertSorted(MakeSched(3, new TimeSpan(8, 0, 0)));
            var all = list.GetAll();
            Assert.AreEqual(new TimeSpan(7, 0, 0), all[0].DepartureTime);
            Assert.AreEqual(new TimeSpan(8, 0, 0), all[1].DepartureTime);
            Assert.AreEqual(new TimeSpan(9, 0, 0), all[2].DepartureTime);
        }

        [TestMethod]
        public void TimetableList_GetByRoute_ReturnsOnlyMatchingRoute()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(7, 0, 0), routeId: 1));
            list.InsertSorted(MakeSched(2, new TimeSpan(8, 0, 0), routeId: 2));
            list.InsertSorted(MakeSched(3, new TimeSpan(9, 0, 0), routeId: 1));
            var results = list.GetByRoute(1);
            Assert.AreEqual(2, results.Length);
        }

        private static Schedule MakeSched(int id, TimeSpan dep, int routeId = 1)
            => new Schedule(id, routeId, "Route", dep, dep.Add(TimeSpan.FromMinutes(20)), 50, 0);
    }
}
