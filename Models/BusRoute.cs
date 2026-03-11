
namespace BusTimetable.models
{

    public class BusRoute
    {

        // This code block represents a bus route that is connecting to multiple bus stops. It contains the route ID, name, and description.

        public int RouteId { get; set; }

        public string RouteName { get; set; } = string.Empty;

        public string RouteDescription { get; set; }

        public BusRoute() { }



    }
}