using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.DataStructures;
using BusTimetable.Models;

namespace BusTimetable.Tests
{
    // tests for the hash table data structure
    [TestClass]
    public class BusStopHashTableTests
    {
        // helper to make a stop quickly without repeating constructor each time
        private BusStop MakeStop(int id, string name)
        {
            return new BusStop(id, name, "Test Area", 51.5, -0.1);
        }

        [TestMethod]
        public void Add_SingleStop_CountIsOne()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(1, "Victoria Station"));
            Assert.AreEqual(1, table.Count);
        }

        [TestMethod]
        // adding same ID twice should update not duplicate
        public void Add_DuplicateId_CountStaysOne()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(1, "Victoria Station"));
            table.Add(MakeStop(1, "Victoria Updated"));
            Assert.AreEqual(1, table.Count);
        }

        [TestMethod]
        public void GetById_ExistingStop_ReturnsCorrectStop()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(5, "Bank"));
            var result = table.GetById(5);
            Assert.IsNotNull(result);
            Assert.AreEqual("Bank", result.StopName);
        }

        [TestMethod]
        public void GetById_StopNotInTable_ReturnsNull()
        {
            var table = new BusStopHashTable();
            var result = table.GetById(999);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByName_ExactMatch_ReturnsCorrectStop()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(1, "Oxford Circus"));
            var result = table.GetByName("Oxford Circus");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.StopID);
        }

        [TestMethod]
        // search should work regardless of capitalisation
        public void GetByName_CaseInsensitive_StillFindsStop()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(1, "Oxford Circus"));
            var result = table.GetByName("oxford circus");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchByName_PartialMatch_ReturnsBothResults()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(1, "Victoria Station"));
            table.Add(MakeStop(2, "Victoria Park"));
            table.Add(MakeStop(3, "London Bridge"));

            var results = table.SearchByName("Victoria");
            Assert.AreEqual(2, results.Length);
        }

        [TestMethod]
        public void Remove_ExistingStop_CountDecreasesAndReturnsTrue()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(1, "Victoria Station"));
            table.Add(MakeStop(2, "Waterloo"));

            bool removed = table.Remove(1);
            Assert.IsTrue(removed);
            Assert.AreEqual(1, table.Count);
        }

        [TestMethod]
        public void Remove_StopNotInTable_ReturnsFalse()
        {
            var table = new BusStopHashTable();
            bool removed = table.Remove(999);
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void GetAll_ThreeStopsAdded_ReturnsAllThree()
        {
            var table = new BusStopHashTable();
            table.Add(MakeStop(1, "Stop A"));
            table.Add(MakeStop(2, "Stop B"));
            table.Add(MakeStop(3, "Stop C"));

            var all = table.GetAll();
            Assert.AreEqual(3, all.Length);
        }
    }

    // tests for the timetable linked list
    [TestClass]
    public class TimetableListTests
    {
        private Schedule MakeSchedule(int id, int routeId, string dep, string arr)
        {
            return new Schedule(id, routeId, "Route " + routeId, TimeSpan.Parse(dep), TimeSpan.Parse(arr), 50);
        }

        [TestMethod]
        // insert out of order and check they come back sorted by time
        public void InsertSorted_OutOfOrder_ComesBackSorted()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSchedule(3, 1, "09:00", "09:30"));
            list.InsertSorted(MakeSchedule(1, 1, "07:00", "07:30"));
            list.InsertSorted(MakeSchedule(2, 1, "08:00", "08:30"));

            var all = list.GetAll();
            Assert.AreEqual(3, all.Length);
            Assert.AreEqual(TimeSpan.Parse("07:00"), all[0].DepartureTime);
            Assert.AreEqual(TimeSpan.Parse("08:00"), all[1].DepartureTime);
            Assert.AreEqual(TimeSpan.Parse("09:00"), all[2].DepartureTime);
        }

        [TestMethod]
        public void GetById_ScheduleExists_ReturnsIt()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSchedule(10, 2, "10:00", "10:20"));
            var result = list.GetById(10);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.ScheduleID);
        }

        [TestMethod]
        public void GetById_ScheduleNotFound_ReturnsNull()
        {
            var list = new TimetableList();
            Assert.IsNull(list.GetById(999));
        }

        [TestMethod]
        public void GetByRoute_TwoRoutesInList_ReturnsOnlyMatchingRoute()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSchedule(1, 1, "07:00", "07:30"));
            list.InsertSorted(MakeSchedule(2, 2, "08:00", "08:30"));
            list.InsertSorted(MakeSchedule(3, 1, "09:00", "09:30"));

            var route1 = list.GetByRoute(1);
            Assert.AreEqual(2, route1.Length);
        }

        [TestMethod]
        public void Remove_ScheduleExists_CountDecreases()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSchedule(1, 1, "07:00", "07:30"));
            list.InsertSorted(MakeSchedule(2, 1, "08:00", "08:30"));

            bool removed = list.Remove(1);
            Assert.IsTrue(removed);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void GetFromTime_StartAt9_Returns9AndLater()
        {
            var list = new TimetableList();
            list.InsertSorted(MakeSchedule(1, 1, "07:00", "07:30"));
            list.InsertSorted(MakeSchedule(2, 1, "09:00", "09:30"));
            list.InsertSorted(MakeSchedule(3, 1, "11:00", "11:30"));

            var results = list.GetFromTime(TimeSpan.Parse("09:00"));
            Assert.AreEqual(2, results.Length);
        }

        [TestMethod]
        public void Update_ChangeSeatsBooked_ValueIsUpdated()
        {
            var list = new TimetableList();
            var schedule = MakeSchedule(1, 1, "07:00", "07:30");
            list.InsertSorted(schedule);

            schedule.SeatsBooked = 5;
            list.Update(schedule);

            var updated = list.GetById(1);
            Assert.AreEqual(5, updated!.SeatsBooked);
        }
    }

    // tests for the ticket linked list
    [TestClass]
    public class TicketListTests
    {
        private Ticket MakeTicket(int id, int passengerId, string status = "Active")
        {
            return new Ticket(id, passengerId, "Test User", 1, "Route 1 07:00-07:30", DateTime.Now, 3.50m, status);
        }

        [TestMethod]
        public void Add_OneTicket_CountIsOne()
        {
            var list = new TicketList();
            list.Add(MakeTicket(1, 1));
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void GetById_TicketExists_ReturnsIt()
        {
            var list = new TicketList();
            list.Add(MakeTicket(1, 1));
            var result = list.GetById(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetByPassenger_TwoPassengers_ReturnsOnlyCorrectPassenger()
        {
            var list = new TicketList();
            list.Add(MakeTicket(1, 1));
            list.Add(MakeTicket(2, 2));
            list.Add(MakeTicket(3, 1));

            var mine = list.GetByPassenger(1);
            Assert.AreEqual(2, mine.Length);
        }

        [TestMethod]
        public void Cancel_ActiveTicket_StatusIsCancelled()
        {
            var list = new TicketList();
            list.Add(MakeTicket(1, 1));
            bool ok = list.Cancel(1);
            Assert.IsTrue(ok);
            Assert.AreEqual("Cancelled", list.GetById(1)!.Status);
        }

        [TestMethod]
        // shouldnt be able to cancel something thats already cancelled
        public void Cancel_AlreadyCancelled_ReturnsFalse()
        {
            var list = new TicketList();
            list.Add(MakeTicket(1, 1, "Cancelled"));
            bool ok = list.Cancel(1);
            Assert.IsFalse(ok);
        }
    }
}
