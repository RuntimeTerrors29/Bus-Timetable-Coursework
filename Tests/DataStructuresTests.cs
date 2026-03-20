using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;
using BusTimetable.DataStructures;

namespace BusTimetable.Tests
{
    [TestClass]
    public class DataStructuresTests
    {
        // ── BusStopHashTable ──────────────────────────────────────────────────

        [TestMethod]
        public void HashTable_Add_MultipleStops_AllRetrievable()
        {
            var table = new BusStopHashTable(8); // small size forces collisions
            for (int i = 1; i <= 10; i++)
                table.Add(new BusStop(i, $"Stop {i}", "Location", 0, 0));

            Assert.AreEqual(10, table.Count);
            for (int i = 1; i <= 10; i++)
                Assert.IsNotNull(table.GetById(i));
        }

        [TestMethod]
        public void HashTable_GetAll_ReturnsAllStops()
        {
            var table = new BusStopHashTable();
            table.Add(new BusStop(1, "A", "Loc", 0, 0));
            table.Add(new BusStop(2, "B", "Loc", 0, 0));
            table.Add(new BusStop(3, "C", "Loc", 0, 0));
            var all = table.GetAll();
            Assert.AreEqual(3, all.Length);
        }

        [TestMethod]
        public void HashTable_Remove_NonExistentId_ReturnsFalse()
        {
            var table = new BusStopHashTable();
            Assert.IsFalse(table.Remove(999));
        }
    }
}
