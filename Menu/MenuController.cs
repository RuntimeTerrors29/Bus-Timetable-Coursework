using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.models;
using BusTimetable.DataStructures;

namespace BusTimetable.Tests
{
    [TestClass]
    public  class  DataStructuresTests
    {

        // BusStopHashTable tests 
        [TestMethod]
        public  void  HashTable_Add_MultipleStops_AllRetrievable()
        {
            var  table = new BusStopHashTable(8);
            for  (int i = 1; i <= 10; i++)
                table.Add(new BusStop(i, $"Stop {i}", "Location", 0, 0));
            Assert.AreEqual(10, table.Count);
            for  (int i = 1; i <= 10; i++)
                Assert.IsNotNull(table.GetById(i));
        }

        [TestMethod]
        public void  HashTable_GetAll_ReturnsAllStops()
        {
            var table = new BusStopHashTable();
            table.Add(new BusStop(1, "A", "L", 0, 0));
            table.Add(new BusStop(2, "B", "L", 0, 0));
            Assert.AreEqual(2, table.GetAll().Length);
        }

        [TestMethod]
        public void  HashTable_Remove_NonExistentId_ReturnsFalse()
        {
            Assert.IsFalse(new  BusStopHashTable().Remove(999));
        }


        // TimetableList tests


        [TestMethod]
        public void HashTable_EmptyTable_CountIsZero()
        {
            Assert.AreEqual(0, new BusStopHashTable().Count);
        }

        [TestMethod]
        public void  TimetableList_InsertSorted_MaintainsDepartureOrder()
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
        public void  TimetableList_GetByRoute_ReturnsOnlyMatchingRoute()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(7, 0,  0), routeId: 1));
            list.InsertSorted(MakeSched(2, new TimeSpan(8, 0, 0), routeId: 2));
            list.InsertSorted(MakeSched(3, new TimeSpan(9, 0, 0), routeId: 1));
            Assert.AreEqual(2, list.GetByRoute(1).Length);
        }

        [TestMethod]
        public void TimetableList_GetBetween_ReturnsCorrectRange()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1, new TimeSpan(7,  0, 0)));
            list.InsertSorted(MakeSched(2, new TimeSpan(8, 0, 0)));
            list.InsertSorted(MakeSched(3, new TimeSpan(9, 0, 0)));
            list.InsertSorted(MakeSched(4, new TimeSpan(10, 0, 0)));
            Assert.AreEqual(2, list.GetBetween(new TimeSpan(8 , 0, 0), new TimeSpan(9, 0, 0)).Length);
        }

        [TestMethod]

        // This test assumes Remove returns true if a node was removed and false if not found
        public void  TimetableList_Remove_MiddleNode_CountDecreases()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSched(1,  new TimeSpan(7, 0, 0)));
            list.InsertSorted(MakeSched(2, new  TimeSpan(8, 0, 0)));
            list.InsertSorted(MakeSched(3, new  TimeSpan(9, 0, 0)));
            Assert.IsTrue(list.Remove(2));
            Assert.AreEqual(2, list.Count);
        }

        // TicketList tests
        [TestMethod]
        public void TicketList_Add_IncreasesCount()
        {
            var list = new TicketList();
            list.Add(MakeTicket(1, 1));
            list.Add(MakeTicket(2, 1));
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TicketList_GetByPassenger_ReturnsOnlyPassengersTickets()
        {
            var list = new TicketList();
            list.Add(MakeTicket(1, 1));
            list.Add(MakeTicket(2, 1));
            list.Add(MakeTicket(3, 2));
            Assert.AreEqual(2, list.GetByPassenger(1).Length);
        }

        
        [TestMethod]
        public void TicketList_Cancel_AlreadyCancelled_ReturnsFalse()
        {
            var list  = new TicketList();
            list.Add(new  Ticket(1, 1, "A", 1, "R", DateTime.Now, 3.5m, "Cancelled"));
            Assert.IsFalse(list.Cancel(1));
        }

        [TestMethod]
        public void  TicketList_GetAll_ReturnsAllTickets()
        {
             var list = new TicketList();
            list.Add(MakeTicket(1, 1));
            list.Add(MakeTicket(2, 2));
            list.Add(MakeTicket(3, 1));
            Assert.AreEqual(3, list.GetAll().Length);
        }

        //  helper methods
        private static Schedule MakeSched(int id, TimeSpan dep, int routeId = 1)
            => new Schedule(id, routeId, "Route", dep, dep.Add(TimeSpan.FromMinutes(20)), 50, 0);

        private static Ticket MakeTicket(int id, int passId)
            => new Ticket(id, passId, "Name", 1, "Route", DateTime.Now, 3.50m);
    }
}
