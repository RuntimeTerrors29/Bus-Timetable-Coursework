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
            table.Add(new BusStop(1,"A","L",0,0)); table.Add(new BusStop(2,"B","L",0,0));
            Assert.AreEqual(2, table.GetAll().Length);
        }

        [TestMethod]
        public void HashTable_Remove_NonExistentId_ReturnsFalse() => Assert.IsFalse(new BusStopHashTable().Remove(999));

        [TestMethod]
        public void HashTable_EmptyTable_CountIsZero() => Assert.AreEqual(0, new BusStopHashTable().Count);

        [TestMethod]
        public void HashTable_SearchByName_EmptyQuery_ReturnsAll()
        {
            var table = new BusStopHashTable();
            table.Add(new BusStop(1,"Victoria","W",0,0));
            table.Add(new BusStop(2,"Waterloo","L",0,0));
            var results = table.SearchByName("");
            Assert.AreEqual(2, results.Length);
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
            Assert.AreEqual(new TimeSpan(9, 0, 0), all[2].DepartureTime);
        }

        [TestMethod]
        public void TimetableList_GetByRoute_ReturnsOnlyMatchingRoute()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(7, 0, 0), routeId: 1));
            list.InsertSorted(MakeSched(2, new TimeSpan(8, 0, 0), routeId: 2));
            list.InsertSorted(MakeSched(3, new TimeSpan(9, 0, 0), routeId: 1));
            Assert.AreEqual(2, list.GetByRoute(1).Length);
        }

        [TestMethod]
        public void TimetableList_GetBetween_ReturnsCorrectRange()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(7, 0, 0)));
            list.InsertSorted(MakeSched(2, new TimeSpan(8, 0, 0)));
            list.InsertSorted(MakeSched(3, new TimeSpan(9, 0, 0)));
            list.InsertSorted(MakeSched(4, new TimeSpan(10, 0, 0)));
            Assert.AreEqual(2, list.GetBetween(new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0)).Length);
        }

        [TestMethod]
        public void TimetableList_Remove_MiddleNode_CountDecreases()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(7, 0, 0)));
            list.InsertSorted(MakeSched(2, new TimeSpan(8, 0, 0)));
            list.InsertSorted(MakeSched(3, new TimeSpan(9, 0, 0)));
            bool removed = list.Remove(2);
            Assert.IsTrue(removed);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TimetableList_Update_ChangesExistingNode()
        {
            var list = new TimetableList();
            var sched = MakeSched(1, new TimeSpan(7, 0, 0));
            list.InsertSorted(sched);
            sched.Capacity = 99;
            list.Update(sched);
            Assert.AreEqual(99, list.GetById(1)!.Capacity);
        }

        private static Schedule MakeSched(int id, TimeSpan dep, int routeId = 1)
            => new Schedule(id, routeId, "Route", dep, dep.Add(TimeSpan.FromMinutes(20)), 50, 0);
    }
}
