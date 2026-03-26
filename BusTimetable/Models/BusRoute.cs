
namespace BusTimetable.Models

{
    public class BusRoute


    {
        // properties of the bus route, including the route ID, name, and description

        public int RouteId { get; set; }
        public  string RouteName { get; set; } = string.Empty;

        
        public  string RouteDescription { get; set; } =  string.Empty;


        // default constructor for the bus route , which initializes the properties to default values

        public BusRoute() { }

        
        public BusRoute(int routeId, string routeName, string routeDescription)
        {

            RouteId = routeId;
            RouteName = routeName;
            RouteDescription = routeDescription;
        }


        // override the ToString method to provide a string representation of the bus route, which includes the route ID and name //
        public override string ToString()
        {
            return RouteId + " - " + RouteName;
        }
    }
}