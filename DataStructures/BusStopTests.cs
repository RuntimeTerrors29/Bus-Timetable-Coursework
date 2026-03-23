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
        public void Setup() => _table = new BusStopHashTable(16);

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
        public void GetById_NonExistingStop_ReturnsNull() => Assert.IsNull(_table.GetById(999));

        [TestMethod]
        public void SearchByName_PartialMatch_ReturnsMatches()
        {
            _table.Add(new BusStop(1, "Victoria Station", "Westminster", 0, 0));
            _table.Add(new BusStop(2, "Victoria Park", "Hackney", 0, 0));
            _table.Add(new BusStop(3, "London Bridge", "Southwark", 0, 0));
            Assert.AreEqual(2, _table.SearchByName("victoria").Length);
        }

        [TestMethod]
        public void Remove_ExistingStop_CountDecreases()
        {
            _table.Add(new BusStop(1, "Victoria", "Westminster", 0, 0));
            Assert.IsTrue(_table.Remove(1));
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

        [TestMethod]
        public void GetByName_CaseInsensitive_ReturnsStop()
        {
            _table.Add(new BusStop(1, "Victoria Station", "Westminster", 0, 0));
            Assert.IsNotNull(_table.GetByName("VICTORIA STATION"));
        }

        [TestMethod]
        public void Remove_AfterAdd_TableIsEmpty()
        {
            _table.Add(new BusStop(5, "Bank", "City", 0, 0));
            _table.Remove(5);
            Assert.AreEqual(0, _table.Count);
            Assert.IsNull(_table.GetById(5));
        }
    }
}
