using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTimetable.Models;

namespace BusTimetable.Tests
{
    [TestClass]
    public class BusRouteTests
    {
        [TestMethod]
        public void Test_RouteID_Correct()
        {
            var route = new BusRoute(1, "Route 12", "Central London");
            Assert.AreEqual(1, route.RouteID);
        }

        [TestMethod]
        public void Test_RouteName_Correct()
        {
            var route = new BusRoute(2, "Route 25", "North London");
            Assert.AreEqual("Route 25", route.RouteName);
        }

        [TestMethod]
        public void Test_Description_Correct()
        {
            var route = new BusRoute(1, "Route 25", "Central London to North London");
            Assert.AreEqual("Central London to North London", route.Description);
        }

        [TestMethod]
        // empty route name should still be stored as empty not null
        public void Test_RouteName_Empty()
        {
            var route = new BusRoute(3, "", "Central London");
            Assert.AreEqual("", route.RouteName);
        }

        [TestMethod]
        public void Test_Description_Default_Empty()
        {
            var route = new BusRoute();
            Assert.AreEqual(string.Empty, route.Description);
        }

        [TestMethod]
        public void Test_RouteID_Zero_Or_Negative()
        {
            var routeZero = new BusRoute(0, "Route 40", "Central London");
            Assert.AreEqual(0, routeZero.RouteID);

            var routeNeg = new BusRoute(-1, "Route 50", "North London");
            Assert.AreEqual(-1, routeNeg.RouteID);
        }
    }
}
