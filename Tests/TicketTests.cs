using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.models;
using BusTimetable.DataStructures;

namespace BusTimeTable.Tests
{
    [TestClass]
    public class TicketTests
    {
        // Test to check  the Ticket class constructor
        [TestMethod]

        public void Ticket_IsActive_WhenStatusIsActive()
        {
            var ticket = new Ticket(1, 1, "Alice", 1, "Route 1", DateTime.Now, 3.50m, "Active");
            Assert.IsTrue(ticket.IsActive);
        }



        // Testing the if  IsActive returns false when the status is "Cancelled"
        [TestMethod]

        public void Ticket_isActive_FalseWhenisCancelled() {


            var ticket = new Ticket(1, 1, "Alice", 1, "Route 1", DateTime.Now, 3.50m, "Cancelled");
            Assert.IsFalse(ticket.IsActive);

        }

        // Testing the Cancel method of the TicketList class to ensure it sets the status of a ticket to "Cancelled"

        [TestMethod]

        public void TicketList_Cancel_SetsStatusToCancelled()
        {
            var ticketList = new TicketList();
            ticketList.Add(new Ticket(1, 1, "Alice", 1, "Route 1", DateTime.Now, 3.50m, "Active"));
            bool result = ticketList.Cancel(1);
            Assert.IsTrue(result);
            Assert.AreEqual("Cancelled", ticketList.GetById(1)!.Status);

        }
        // Testing the GetByPassenger method of the TicketList class to ensure it returns the correct tickets for a given passenger name
        [TestMethod]

        public void TicketList_GetByPassenger_ReturnsCorrectTickets()
        {
            var ticketList = new TicketList();
            ticketList.Add(new Ticket(1, 1, "Alice", 1, "Route 1", DateTime.Now, 3.50m, "Active"));
            ticketList.Add(new Ticket(2, 1, "Alice", 2, "Route 2", DateTime.Now, 3.50m, "Active"));
            ticketList.Add(new Ticket(3, 2, "Bob", 3, "Route 3", DateTime.Now, 4.00m, "Active"));
            var aliceTickets = ticketList.GetByPassenger(1);
            Assert.AreEqual(2, aliceTickets.Length);

        }
    }
}


          