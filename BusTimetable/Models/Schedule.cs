namespace BusTimetable.Models
{
	
// Schedule system, it shows a single scheduled departure(s) for a route, it also keeps the max booking to support ticket system.
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int RouteID { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Capacity { get; set; }
        public int SeatsBooked { get; set; }

        public int AvailableSeats => Capacity - SeatsBooked;
        public bool IsFull => SeatsBooked >= Capacity;

        public Schedule() { }
        public Schedule(int scheduleId, int routeId, string routeName,
                        TimeSpan departure, TimeSpan arrival, int capacity, int seatsBooked = 0)
        {
            ScheduleID    = scheduleId;
            RouteID       = routeId;
            RouteName     = routeName;
            DepartureTime = departure;
            ArrivalTime   = arrival;
            Capacity      = capacity;
            SeatsBooked   = seatsBooked;
        }

        public override string ToString()
            => $"[{ScheduleID}] {RouteName}  {DepartureTime:hh\\:mm} -> {ArrivalTime:hh\\:mm}  " +
               $"({SeatsBooked}/{Capacity} booked  |  {AvailableSeats} remaining)";
    }
}
