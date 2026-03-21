using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;
using BusTimetable.DataStructures;

namespace BusTimetable.Tests
{
    [TestClass]
    public class BusStopTests
    {
        private BusStopHashTable _table = null!;

        [TestInitialize]
        public void Setup()
        {
            _table = new BusStopHashTable(16);
        }

        [TestMethod]
        public void Add_SingleStop_CountIsOne()
        {
            _table.Add(new BusStop(1, "Victoria", "Westminster", 51.4952, -0.1441));
            Assert.AreEqual(1, _table.Count);
        }

        [TestMethod]
        public void GetById_ExistingStop_ReturnsStop()
        {
            _table.Add(new BusStop(1, "Victoria", "Westminster", 51.4952, -0.1441));
            var result = _table.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Victoria", result.StopName);
        }

        [TestMethod]
        public void GetById_NonExistingStop_ReturnsNull()
        {
            var result = _table.GetById(999);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchByName_PartialMatch_ReturnsMatches()
        {
            _table.Add(new BusStop(1, "Victoria Station", "Westminster", 0, 0));
            _table.Add(new BusStop(2, "Victoria Park", "Hackney", 0, 0));
            _table.Add(new BusStop(3, "London Bridge", "Southwark", 0, 0));

            var results = _table.SearchByName("victoria");
            Assert.AreEqual(2, results.Length);
        }

        [TestMethod]
        public void Remove_ExistingStop_CountDecreases()
        {
            _table.Add(new BusStop(1, "Victoria", "Westminster", 0, 0));
            bool removed = _table.Remove(1);
            Assert.IsTrue(removed);
            Assert.AreEqual(0, _table.Count);
        }

        [TestMethod]
        public void Add_DuplicateId_UpdatesExisting()
        {
            _table.Add(new BusStop(1, "Old Name", "Location", 0, 0));
            _table.Add(new BusStop(1, "New Name", "Location", 0, 0));
            Assert.AreEqual(1, _table.Count);
            Assert.AreEqual("New Name", _table.GetById(1)!.StopName);
        }
    }
}
