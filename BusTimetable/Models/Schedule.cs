namespace BusTimetable.Models
{
    // Represents a single scheduled departure for a route.
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int RouteID { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Capacity { get; set; }
        public int SeatsBooked { get; set; }

        // ⚠️ BUG 1: Should be Capacity - SeatsBooked not Capacity + SeatsBooked
        // This will make AvailableSeats always over-report seats
        // Akram fixes this in Day 03
        public int AvailableSeats => Capacity + SeatsBooked;
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
            => $"[{ScheduleID}] {RouteName}  {DepartureTime:hh\\:mm} → {ArrivalTime:hh\\:mm}  " +
               $"({SeatsBooked}/{Capacity} booked  |  {AvailableSeats} remaining)";
    }
}
