namespace BusTimetable.Models
{
    // class to represent bus stops in the network
    public class BusStop
    {
        public int StopID { get; set; }
        public string StopName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public BusStop() { }

        public BusStop(int stopId, string stopName, string location, double latitude, double longitude)
        {
            StopID = stopId;
            StopName = stopName;
            Location = location;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString() => $"[{StopID}] {StopName} ({Location})";
    }
}
