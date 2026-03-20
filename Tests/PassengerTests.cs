using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;
using BusTimetable.DataStructures;

namespace BusTimetable.Tests
{
    [TestClass]
    public class PassengerTests
    {
        [TestMethod]
        public void Constructor_SetsAllProperties()
        {
            var p = new Passenger(1, "Alice", "alice@example.com");
            Assert.AreEqual(1, p.PassengerID);
            Assert.AreEqual("Alice", p.FullName);
            Assert.AreEqual("alice@example.com", p.Email);
        }

        [TestMethod]
        public void ToString_ContainsNameAndEmail()
        {
            var p = new Passenger(1, "Alice", "alice@example.com");
            Assert.IsTrue(p.ToString().Contains("Alice"));
            Assert.IsTrue(p.ToString().Contains("alice@example.com"));
        }

        [TestMethod]
        public void PassengerList_Add_IncreasesCount()
        {
            var list = new PassengerList();
            list.Add(new Passenger(1, "Alice", "alice@example.com"));
            list.Add(new Passenger(2, "Bob", "bob@example.com"));
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void PassengerList_GetById_ReturnsCorrectPassenger()
        {
            var list = new PassengerList();
            list.Add(new Passenger(1, "Alice", "alice@example.com"));
            list.Add(new Passenger(2, "Bob", "bob@example.com"));
            var result = list.GetById(2);
            Assert.IsNotNull(result);
            Assert.AreEqual("Bob", result.FullName);
        }
    }
}
