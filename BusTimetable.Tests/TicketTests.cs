using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;

namespace BusTimetable.Tests
{
    [TestClass]
    public class TicketTests
    {
        // helper so I dont have to repeat the full constructor in every test
        private Ticket MakeTicket(int id, decimal price, string status = "Active")
        {
            return new Ticket(id, 1, "Test Passenger", 1, "Route 1 07:00-07:30", DateTime.Now, price, status);
        }

        [TestMethod]
        public void Test_TicketID_Correct()
        {
            var ticket = MakeTicket(1, 5.50m);
            Assert.AreEqual(1, ticket.TicketID);
        }

        [TestMethod]
        public void Test_TicketPrice_Correct()
        {
            var ticket = MakeTicket(1, 5.50m);
            Assert.AreEqual(5.50m, ticket.Price);
        }

        [TestMethod]
        public void Test_TicketStatus_DefaultActive()
        {
            var ticket = MakeTicket(1, 5.50m);
            Assert.AreEqual("Active", ticket.Status);
        }

        [TestMethod]
        public void Test_IsActive_True()
        {
            var ticket = MakeTicket(1, 5.50m);
            Assert.IsTrue(ticket.IsActive);
        }

        [TestMethod]
        public void Test_Cancelled_IsActive_False()
        {
            var ticket = MakeTicket(1, 5.50m, "Cancelled");
            Assert.IsFalse(ticket.IsActive);
        }

        [TestMethod]
        // negative price shouldnt crash, model doesnt validate business rules
        public void Test_Price_CanBeNegative()
        {
            var ticket = MakeTicket(1, -5.50m);
            Assert.AreEqual(-5.50m, ticket.Price);
        }
    }
}
