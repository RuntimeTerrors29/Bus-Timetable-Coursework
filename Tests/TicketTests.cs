using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.models;
using BusTimetable.DataStructures;

namespace BusTimetable.Tests
{

    [TestClass]
    public class TicketTests


    {
        // edge case - check IsActive when status is "Active"
        [TestMethod]
        public void  Ticket_IsActive_WhenStatusIsActive()
        { 
            var  ticket = new Ticket(1, 1, "Alice", 1, "Route 1", DateTime.Now, 3.50m, "Active");
            Assert.IsTrue(ticket.IsActive);
        }

        
        [TestMethod]
        public void  Ticket_isActive_FalseWhenisCancelled()
        {
            var ticket  =  new Ticket(1, 1, "Alice", 1,  "Route 1 ", DateTime.Now , 3.50m, "Cancelled");
            Assert.IsFalse(ticket.IsActive);
        }


        // edge case - check IsActive when status is something unexpected
        [TestMethod]
        public void  TicketList_Cancel_SetsStatusToCancelled()
        {
            var ticketList = new TicketList();
            ticketList.Add(new Ticket(1, 1, "Alice", 1, "Route 1", DateTime.Now, 3.50m, "Active"));
            bool result = ticketList.Cancel(1);
            Assert.IsTrue(result);
            Assert.AreEqual("Cancelled", ticketList.GetById(1)!.Status);
        }

        [TestMethod]

        // edge case - cancel ticket that is already cancelled
        public void TicketList_Cancel_AlreadyCancelled_ReturnsFalse()
        {
            var ticketList = new TicketList();
            ticketList.Add(new Ticket(1, 1, "Alice", 1, "R1", DateTime.Now, 3.50m, "Cancelled"));
            Assert.IsFalse(ticketList.Cancel(1));
        }

        // edge case - cancel ticket that doesnt exist
        [TestMethod]
        public void TicketList_Cancel_NonExistent_ReturnsFalse()
        {
            Assert.IsFalse(new TicketList().Cancel(999));
        }

         [TestMethod]
        public void  TicketList_GetByPassenger_ReturnsCorrectTickets()
        {
            var ticketList = new TicketList();
            ticketList.Add(new Ticket(1, 1, "Alice",  1, "Route 1", DateTime.Now, 3.50m));
            ticketList.Add(new Ticket(2, 1, "Alice", 2, "Route  2", DateTime.Now, 3.50m));
            ticketList.Add(new Ticket(3, 2, "Bob", 3, "Route 3",  DateTime.Now, 4.00m));
            var aliceTickets = ticketList.GetByPassenger(1);
            Assert.AreEqual(2, aliceTickets.Length);
        }

        // edge case - check ToString contains ticket  ID
        [TestMethod]
        public void  TicketList_ToString_ContainsTicketId()
        {
            var t = new Ticket(42,  1, "Alice", 1, "Route 1", DateTime.Now, 3.50m);
            Assert.IsTrue(t.ToString().Contains("42"));
        }
    }
}


