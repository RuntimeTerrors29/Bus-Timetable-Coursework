using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;

namespace BusTimetable.Tests
{
    [TestClass]
    public class BusRouteTests
    {
        [TestMethod]
        public void Constructor_SetsAllProperties()
        {
            var route = new BusRoute(1, "Route 1", "Victoria to Bank");
            Assert.AreEqual(1, route.RouteID);
            Assert.AreEqual("Route 1", route.RouteName);
            Assert.AreEqual("Victoria to Bank", route.Description);
        }

        [TestMethod]
        public void Constructor_DefaultDescription_IsEmpty()
        {
            var route = new BusRoute(2, "Route 2");
            Assert.AreEqual(string.Empty, route.Description);
        }

        [TestMethod]
        public void ToString_ContainsRouteNameAndDescription()
        {
            var route = new BusRoute(1, "Route 1", "Express");
            var str   = route.ToString();
            Assert.IsTrue(str.Contains("Route 1"));
            Assert.IsTrue(str.Contains("Express"));
        }
    }
}
