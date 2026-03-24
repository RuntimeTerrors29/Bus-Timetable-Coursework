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
            var p = new Passenger(1, "Stefan", "stefan@email.com");
            Assert.AreEqual(1, p.PassengerID);
            Assert.AreEqual("Stefan", p.FullName);
            Assert.AreEqual("stefan@email.com", p.Email);
        }

        [TestMethod]
        public void ToString_ContainsNameAndEmail()
        {
            var p = new Passenger(1, "Stefan", "stefan@email.com");
            Assert.IsTrue(p.ToString().Contains("Stefan"));
            Assert.IsTrue(p.ToString().Contains("stefan@email.com"));
        }

        [TestMethod]
        public void PassengerList_Add_IncreasesCount()
        {
            var list = new PassengerList();
            list.Add(new Passenger(1, "Stefan", "stefan@email.com"));
            list.Add(new Passenger(2, "Ugur", "ugur@email.com"));
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void PassengerList_GetById_ReturnsCorrectPassenger()
        {
            var list = new PassengerList();
            list.Add(new Passenger(1, "Stefan", "stefan@email.com"));
            list.Add(new Passenger(2, "Ugur", "ugur@email.com"));
            Assert.AreEqual("Bob", list.GetById(2)!.FullName);
        }

        [TestMethod]
        public void PassengerList_EmptyList_CountIsZero()
        {
            Assert.AreEqual(0, new PassengerList().Count);
        }

        [TestMethod]
        public void PassengerList_GetById_NotFound_ReturnsNull()
        {
            Assert.IsNull(new PassengerList().GetById(999));
        }
    }
}
