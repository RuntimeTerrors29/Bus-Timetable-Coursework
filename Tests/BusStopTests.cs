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
    }
}
