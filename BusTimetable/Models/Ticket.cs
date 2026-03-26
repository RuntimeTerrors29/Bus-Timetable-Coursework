namespace BusTimetable.Models
{
    // represents a ticket/booking for a passenger on a scheduled service
    // IsActive is true when Status == "Active"
    public class Ticket
    {
        public int TicketID { get; set; }
        public int PassengerID { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public int ScheduleID { get; set; }
        public string ScheduleInfo { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = "Active";

	// quick check instead of comparing string every time
        public bool IsActive => Status == "Active";

        public Ticket() { }
        public Ticket(int ticketId, int passengerId, string passengerName,
                      int scheduleId, string scheduleInfo,
                      DateTime bookingDate, decimal price, string status = "Active")
        {
            TicketID = ticketId;
            PassengerID = passengerId;
            PassengerName = passengerName;
            ScheduleID = scheduleId;
            ScheduleInfo = scheduleInfo;
            BookingDate = bookingDate;
            Price = price;
            Status = status;
        }

        public override string ToString()
        {
            return $"Ticket #{TicketID} | {PassengerName} | {ScheduleInfo} | £{Price:F2} | {Status}";
        }
    }
}