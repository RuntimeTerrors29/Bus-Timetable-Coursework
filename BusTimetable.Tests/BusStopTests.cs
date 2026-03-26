using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;

namespace BusTimetable.Tests
{
    [TestClass]
    public class BusStopTests
    {
        [TestMethod]
        public void Test_StopID_Correct()
        {
            var stop = new BusStop(1, "Victoria Station", "Central London", 51.4952, -0.1441);
            Assert.AreEqual(1, stop.StopID);
        }

        [TestMethod]
        public void Test_StopName_Correct()
        {
            var stop = new BusStop(1, "Victoria Station", "Central London", 51.4952, -0.1441);
            Assert.AreEqual("Victoria Station", stop.StopName);
        }

        [TestMethod]
        // check it accepts empty names without crashing
        public void Test_StopName_Empty()
        {
            var stop = new BusStop(2, "", "Central London", 51.4952, -0.1441);
            Assert.AreEqual("", stop.StopName);
        }

        [TestMethod]
        public void Test_Location_Correct()
        {
            var stop = new BusStop(1, "Victoria Station", "Central London", 51.4952, -0.1441);
            Assert.AreEqual("Central London", stop.Location);
        }

        [TestMethod]
        public void Test_Latitude_Correct()
        {
            var stop = new BusStop(1, "Victoria Station", "Central London", 51.4952, -0.1441);
            Assert.AreEqual(51.4952, stop.Latitude);
        }

        [TestMethod]
        public void Test_Longitude_Correct()
        {
            var stop = new BusStop(1, "Victoria Station", "Central London", 51.4952, -0.1441);
            Assert.AreEqual(-0.1441, stop.Longitude);
        }
    }
}
