using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.models;
using BusTimetable.DataStructures;


namespace BusTimetable.Tests
{
    [TestClass]
    public class DataStructuresTests
    {
        [TestMethod]
        public  void HashTable_Add_MultipleStops_AllRetrievable()
        {
            var table  = new BusStopHashTable(8);
            for  (int i = 1; i <= 10; i++)
                 table.Add(new BusStop(i, $"Stop {i}", "Location", 0, 0));
            Assert.AreEqual(10, table.Count);
            for  (int i = 1; i <= 10; i++)
                Assert.IsNotNull(table.GetById(i));
        }


        // Test to check that the GetAll method of the BusStopHashTable class returns all added bus stops
        [TestMethod]
        public void HashTable_GetAll_ReturnsAllStops()
        {
            var table = new BusStopHashTable();
            table.Add(new BusStop(1, "A", "Loc", 0, 0));
            table.Add(new BusStop(2, "B", "Loc", 0, 0));
            Assert.AreEqual(2, table.GetAll().Length);
        }

        [TestMethod]
        public void HashTable_Remove_NonExistentId_ReturnsFalse()
        {
            Assert.IsFalse(new BusStopHashTable().Remove(999));
        }

        // Test to check that the Remove method of the BusStopHashTable class returns false when trying to remove a non-existent bus stop
        [TestMethod]
        public void TimetableList_InsertSorted_MaintainsDepartureOrder()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(9, 0, 0)));
            list.InsertSorted(MakeSched(2, new TimeSpan(7, 0, 0)));
            list.InsertSorted(MakeSched(3, new TimeSpan(8, 0, 0)));
            var all = list.GetAll();
            Assert.AreEqual(new TimeSpan(7, 0, 0), all[0].DepartureTime);
        }

        [TestMethod]
        public void TimetableList_GetByRoute_ReturnsOnlyMatchingRoute()
        {
            var  list = new TimetableList();
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
            var results = list.GetBetween(new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0));
            Assert.AreEqual(2, results.Length);
        }


        [TestMethod]
        // Test to check that the GetFromTime method of the TimetableList class returns only schedules departing at or after the specified time
        public void TimetableList_GetFromTime_ExcludesEarlierServices()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(7, 0, 0)));
            list.InsertSorted(MakeSched(2, new TimeSpan(9, 0, 0)));
            list.InsertSorted(MakeSched(3, new TimeSpan(11, 0, 0)));
            var results = list.GetFromTime(new TimeSpan(9, 0, 0));
            Assert.AreEqual(2, results.Length);
        }



        // helper method to create a schedule for testing
        private static Schedule MakeSched(int id, TimeSpan dep, int routeId = 1)
            => new Schedule(id, routeId, "Route", dep, dep.Add(TimeSpan.FromMinutes(20)), 50, 0);
    }
}
