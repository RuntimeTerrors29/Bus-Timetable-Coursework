using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;

namespace BusTimetable.Tests
{
    [TestClass]
    public class PassengerTests
    {
        [TestMethod]
        public void Test_PassengerID_Correct()
        {
            var passenger = new Passenger(1, "John Doe", "john@mail.com");
            Assert.AreEqual(1, passenger.PassengerID);
        }

        [TestMethod]
        public void Test_FullName_Correct()
        {
            var passenger = new Passenger(1, "Jane Smith", "jane@mail.com");
            Assert.AreEqual("Jane Smith", passenger.FullName);
        }

        [TestMethod]
        public void Test_Email_Correct()
        {
            var passenger = new Passenger(1, "John Doe", "john@mail.com");
            Assert.AreEqual("john@mail.com", passenger.Email);
        }

        [TestMethod]
        public void Test_FullName_Empty()
        {
            var passenger = new Passenger(1, "", "john@mail.com");
            Assert.AreEqual("", passenger.FullName);
        }

        [TestMethod]
        public void Test_Email_Empty()
        {
            var passenger = new Passenger(1, "John Doe", "");
            Assert.AreEqual("", passenger.Email);
        }

        [TestMethod]
        // edge case - what happens with 0 or negative IDs
        public void Test_PassengerID_NegativeOrZero()
        {
            var p0 = new Passenger(0, "John Doe", "john@mail.com");
            Assert.AreEqual(0, p0.PassengerID);

            var pNeg = new Passenger(-1, "John Doe", "john@mail.com");
            Assert.AreEqual(-1, pNeg.PassengerID);
        }
    }
}
